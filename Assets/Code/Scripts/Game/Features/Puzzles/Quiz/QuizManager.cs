namespace ProjectM.Puzzles.Quiz
{
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Features.Main.PointsSystem.Rewards;
    using VUDK.Extensions;
    using VUDK.Features.More.GameTaskSystem;
    using VUDK.Features.Main.TimerSystem.Events;
    using ProjectM.Constants;
    using ProjectM.Features.Puzzles.Quiz.Data;
    using ProjectM.Features.Puzzles.Quiz.UI;
    using ProjectM.Features.Puzzles.Quiz.Data.SaveData;
    using UnityEngine.Localization;

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

        public override void Init()
        {
            base.Init();
            if (IsInProgress) // If puzzle is in progress, load the saved data
            {
                _currentQuizIndex = SaveValue.QuizIndex;
                _currentQuestionIndex = SaveValue.QuestionIndex;
            }
        }

        //public void BeginQuiz()
        //{
        //    if(IsSolved && !IsRepeatable)
        //        TimerEventsHandler.StartTimerHandler(GetSecondsToWait());

        //    if (!IsInProgress)
        //    {
        //        if (IsSolved && !IsRepeatable)
        //        {
        //            DisplayCompletedQuiz();
        //            return;
        //        }

        //        _currentQuizData = _loadedQuizPool.Pool.GetRandomElement();
        //        BeginTask();
        //    }
        //    else
        //    {
        //        ResumeTask();
        //    }
        //}

        //public override void BeginTask()
        //{
        //    if (_currentQuizData == null) return;
        //    base.BeginTask();

        //    _currentQuestionIndex = 0;
        //    Enable();
        //    DisplayQuestion();
        //}

        public override void BeginTask()
        {
            base.BeginTask();
            _currentQuizData = _loadedQuizPool.Pool.GetRandomElement();
            if (_currentQuizData == null) return;

            _currentQuestionIndex = 0;
            Enable();
            DisplayQuestion();
        }

        public override void ResolveTask()
        {
            base.ResolveTask();
            PrintMessage(_quizCompletedMessage.GetLocalizedString());
        }

        public override void InterruptTask()
        {
            base.InterruptTask();
            Disable();
        }

        public override void ResumeTask()
        {
            base.ResumeTask();
            DisplayQuestion();
            Enable();
        }

        protected override void OnEnterFocusIsSolved()
        {
            base.OnEnterFocusIsSolved();
            DisplayCompletedQuiz();
        }

        public void Enable()
        {
            EventManager.Ins.TriggerEvent(GameEventKeys.OnEnterQuiz);
            _quizPanel.gameObject.SetActive(true);
        }

        public void Disable()
        {
            EventManager.Ins.TriggerEvent(GameEventKeys.OnExitQuiz);
            _quizPanel.gameObject.SetActive(false);
        }

        public void DisplayQuestion()
        {
            if (_isWaitingAnswer || !IsFocused || !IsInProgress) return;
            PrintQuestion();
            DisaplayAnswers();
        }

        private void DisplayQuestion(InputAction.CallbackContext ctx)
        {
            DisplayQuestion();
        }

        private void NextQuestion()
        {
            _currentQuestionIndex++;
            if (_currentQuestionIndex > _currentQuizData.Questions.Count - 1)
                ResolveTask();
            else
                SaveQuizState(); // Saving at every question to prevent data loss in case of crash
        }

        private void EnableChoicesBox()
        {
            _answersBox.gameObject.SetActive(true);
        }

        private void DisableAnswersBox()
        {
            _answersBox.gameObject.SetActive(false);
        }

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

        private void CorrectAnswer()
        {
            OnCorrectAnswer?.Invoke();
            PrintMessage(_correctAnswerMessage.GetLocalizedString());

            if(!IsSolved)
                _rewarder.SendReward(CurrentQuestion.PointsReward);
        }

        private void WrongAnswer()
        {
            OnWrongAnswer?.Invoke();
            PrintMessage(_wrongAnswerMessage.GetLocalizedString());
        }

        private void PrintMessage(string message)
        {
            _questionText.text = message;
        }

        private void PrintQuestion()
        {
            _questionText.text = CurrentQuestion.QuestionText;
        }

        private void DisplayCompletedQuiz()
        {
            Enable();
            PrintMessage(_quizCompletedMessage.GetLocalizedString());
            DisableAnswersBox();
        }

        private void SaveQuizState()
        {
            SaveValue.IsInProgress = IsInProgress;
            SaveValue.QuizIndex = _currentQuizIndex;
            SaveValue.QuestionIndex = _currentQuestionIndex;
            Push();
        }

        private void LoadQuizPoolData(QuizPoolData asset)
        {
            _loadedQuizPool = asset;
        }
    }
}