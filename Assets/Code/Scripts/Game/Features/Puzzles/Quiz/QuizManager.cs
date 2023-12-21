namespace ProjectM.Puzzles.Quiz
{
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using UnityEngine.Events;
    using UnityEngine.Localization;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Features.Main.PointsSystem.Rewards;
    using VUDK.Extensions;
    using VUDK.Features.More.GameTaskSystem;
    using ProjectM.Constants;
    using ProjectM.Features.Puzzles.Quiz.Data;
    using ProjectM.Features.Puzzles.Quiz.UI;
    using ProjectM.Features.Puzzles.Quiz.Data.SaveData;

    [RequireComponent(typeof(Rewarder))]
    public class QuizManager : GameTaskSaverBase<QuizSaveValue>
    {
        [Header("Quiz Messages")]
        [SerializeField]
        private LocalizedString _correctAnswerMessage;
        [SerializeField]
        private LocalizedString _wrongAnswerMessage;
        [SerializeField]
        private LocalizedString _quizCompletedMessage;

        [Header("UI Quiz Panel")]
        [SerializeField]
        private RectTransform _quizPanel;
        [SerializeField]
        private TMP_Text _questionText;

        [Header("UI Buttons")]
        [SerializeField]
        private Button _closeButton;

        [Header("UI Answers")]
        [SerializeField]
        private RectTransform _answersBox;
        [SerializeField]
        private List<UIQuizAnswerButton> _answersButtons;

        [Header("Quiz Data")]
        [SerializeField]
        private LocalizedAsset<QuizPoolData> _quizPool;

        private QuizPoolData _loadedQuizPool;
        private int _currentQuestionIndex;
        private int _currentQuizIndex;
        private bool _isWaitingAnswer;
        private Rewarder _rewarder;

        private QuizData _currentQuizData;
        private QuizQuestionData CurrentQuestion => _currentQuizData.Questions[_currentQuestionIndex];
        private int MaxChoices => _answersButtons.Count;

        [Header("Quiz Events")]
        public UnityEvent OnCorrectAnswer;
        public UnityEvent OnWrongAnswer;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _rewarder);
            Disable();
            LoadQuizPoolData(_quizPool.LoadAsset());
        }

        private void OnEnable()
        {
            _quizPool.AssetChanged += LoadQuizPoolData;
            _closeButton.onClick.AddListener(InterruptTask);
            EventManager.Ins.AddListener<int>(GameEventKeys.OnSelectQuizAnswer, ReceiveAnswer);
            InputsManager.Inputs.Interaction.Interact.canceled += DisplayQuestion;
        }

        private void OnDisable()
        {
            _quizPool.AssetChanged -= LoadQuizPoolData;
            _closeButton.onClick.RemoveListener(InterruptTask);
            EventManager.Ins.RemoveListener<int>(GameEventKeys.OnSelectQuizAnswer, ReceiveAnswer);
            InputsManager.Inputs.Interaction.Interact.canceled -= DisplayQuestion;
        }

        /// <inheritdoc/>
        public override void Init()
        {
            base.Init();
            if (IsInProgress) // If puzzle is in progress, load the saved data
            {
                _currentQuizIndex = SaveValue.QuizIndex;
                _currentQuestionIndex = SaveValue.QuestionIndex;
            }
        }

        /// <inheritdoc/>
        public override void BeginTask()
        {
            base.BeginTask();
            _currentQuizData = _loadedQuizPool.Pool.GetRandomElement();
            if (_currentQuizData == null) return;

            _currentQuestionIndex = 0;
            Enable();
            DisplayQuestion();
        }

        /// <inheritdoc/>
        public override void ResolveTask()
        {
            base.ResolveTask();
            PrintMessage(_quizCompletedMessage.GetLocalizedString());
        }

        /// <inheritdoc/>
        public override void InterruptTask()
        {
            base.InterruptTask();
            Disable();
        }

        /// <inheritdoc/>
        public override void ResumeTask()
        {
            base.ResumeTask();
            DisplayQuestion();
            Enable();
        }

        /// <inheritdoc/>
        protected override void OnEnterFocusIsSolved()
        {
            base.OnEnterFocusIsSolved();
            DisplayCompletedQuiz();
        }

        /// <inheritdoc/>
        public void Enable()
        {
            EventManager.Ins.TriggerEvent(GameEventKeys.OnEnterQuiz);
            _quizPanel.gameObject.SetActive(true);
        }

        /// <inheritdoc/>
        public void Disable()
        {
            EventManager.Ins.TriggerEvent(GameEventKeys.OnExitQuiz);
            _quizPanel.gameObject.SetActive(false);
        }

        /// <summary>
        /// Displays the current question.
        /// </summary>
        public void DisplayQuestion()
        {
            if (_isWaitingAnswer || !IsFocused || !IsInProgress) return;
            PrintQuestion();
            DisaplayAnswers();
        }

        /// <summary>
        /// Displays the current question.
        /// </summary>
        private void DisplayQuestion(InputAction.CallbackContext ctx)
        {
            DisplayQuestion();
        }

        /// <summary>
        /// Goes to the next question.
        /// </summary>
        private void NextQuestion()
        {
            _currentQuestionIndex++;
            if (_currentQuestionIndex > _currentQuizData.Questions.Count - 1)
                ResolveTask();
            else
                SaveQuizState(); // Saving at every question to prevent data loss in case of crash
        }

        /// <summary>
        /// Enables the choices box.
        /// </summary>
        private void EnableChoicesBox()
        {
            _answersBox.gameObject.SetActive(true);
        }

        /// <summary>
        /// Disables the choices box.
        /// </summary>
        private void DisableAnswersBox()
        {
            _answersBox.gameObject.SetActive(false);
        }

        /// <summary>
        /// Displays the answers.
        /// </summary>
        private void DisaplayAnswers()
        {
            _isWaitingAnswer = true;
            EnableChoicesBox();

            int choicesCount = Mathf.Min(CurrentQuestion.Answers.Length, MaxChoices);

            foreach (var choiceButton in _answersButtons) // Disable all buttons
                choiceButton.Disable();

            for (int i = 0; i < choicesCount; i++) // Enable only the buttons that will be used
            {
                _answersButtons[i].Init(i);
                _answersButtons[i].Enable();
                _answersButtons[i].Text.text = $"{i + 1}. {CurrentQuestion.Answers[i].AnswerText}";
            }
        }

        /// <summary>
        /// Receives the answer from the player.
        /// </summary>
        /// <param name="choiceIndex">Index of the choice.</param>
        private void ReceiveAnswer(int choiceIndex)
        {
            if (!_isWaitingAnswer) return;
            _isWaitingAnswer = false;
            DisableAnswersBox();

            if (CurrentQuestion.Answers[choiceIndex].IsCorrect)
                CorrectAnswer();
            else
                WrongAnswer();

            NextQuestion();
        }

        /// <summary>
        /// Called when the player answers correctly.
        /// </summary>
        private void CorrectAnswer()
        {
            OnCorrectAnswer?.Invoke();
            PrintMessage(_correctAnswerMessage.GetLocalizedString());

            if(!IsSolved)
                _rewarder.SendReward(CurrentQuestion.PointsReward);
        }

        /// <summary>
        /// Called when the player answers incorrectly.
        /// </summary>
        private void WrongAnswer()
        {
            OnWrongAnswer?.Invoke();
            PrintMessage(_wrongAnswerMessage.GetLocalizedString());
        }

        /// <summary>
        /// Prints the message.
        /// </summary>
        /// <param name="message">Message to print.</param>
        private void PrintMessage(string message)
        {
            _questionText.text = message;
        }

        /// <summary>
        /// Prints the question.
        /// </summary>
        private void PrintQuestion()
        {
            _questionText.text = CurrentQuestion.QuestionText;
        }

        /// <summary>
        /// Displays the completed quiz.
        /// </summary>
        private void DisplayCompletedQuiz()
        {
            Enable();
            PrintMessage(_quizCompletedMessage.GetLocalizedString());
            DisableAnswersBox();
        }

        /// <summary>
        /// Saves the quiz state.
        /// </summary>
        private void SaveQuizState()
        {
            SaveValue.IsInProgress = IsInProgress;
            SaveValue.QuizIndex = _currentQuizIndex;
            SaveValue.QuestionIndex = _currentQuestionIndex;
            Push();
        }

        /// <summary>
        /// Loads the quiz pool data.
        /// </summary>
        /// <param name="asset"><see cref="QuizPoolData"/> to load.</param>
        private void LoadQuizPoolData(QuizPoolData asset)
        {
            _loadedQuizPool = asset;
        }
    }
}