namespace ProjectM.Puzzles.Quiz
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Features.Main.PointsSystem.Rewards;
    using VUDK.Features.More.PuzzleSystem.Bases;
    using ProjectM.Constants;
    using ProjectM.Features.Puzzles.Quiz.Data;
    using ProjectM.Features.Puzzles.Quiz.UI;

    [RequireComponent(typeof(Rewarder))]
    public class QuizManager : DatePuzzleBase
    {
        [Header("Quiz Messages")]
        [SerializeField]
        private string _correctAnswerMessage;
        [SerializeField]
        private string _wrongAnswerMessage;
        [SerializeField]
        private string _quizCompletedMessage;

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

        private QuizData _currentQuizData;
        private int _currentQuestionIndex;
        private bool _isWaitingAnswer;
        private Rewarder _rewarder;

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
        }

        private void OnEnable()
        {
            _closeButton.onClick.AddListener(InterruptPuzzle);
            EventManager.Ins.AddListener<QuizData>(GameEventKeys.OnQuizTrigger, OnQuizTrigger);
            EventManager.Ins.AddListener<int>(GameEventKeys.OnSelectQuizAnswer, ReceiveAnswer);
            InputsManager.Inputs.Interaction.Interact.canceled += NextQuestion;
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(InterruptPuzzle);
            EventManager.Ins.RemoveListener<QuizData>(GameEventKeys.OnQuizTrigger, OnQuizTrigger);
            EventManager.Ins.RemoveListener<int>(GameEventKeys.OnSelectQuizAnswer, ReceiveAnswer);
            InputsManager.Inputs.Interaction.Interact.canceled -= NextQuestion;
        }

        public void OnQuizTrigger(QuizData quizData)
        {
            if (!IsInProgress)
            {
                if (IsSolved && !IsRepeatable)
                {
                    DisplayCompletedQuiz();
                    return;
                }

                BeginQuiz(quizData);
            }
            else
            {
                ResumePuzzle();
            }
        }

        public void BeginQuiz(QuizData quizData)
        {
            _currentQuizData = quizData;
            BeginPuzzle();
        }

        public override void BeginPuzzle()
        {
            if (_currentQuizData == null) return;
            base.BeginPuzzle();

            _currentQuestionIndex = 0;
            Enable();
            NextQuestion();
        }

        public override void ResolvePuzzle()
        {
            base.ResolvePuzzle();
            PrintMessage(_quizCompletedMessage);
        }

        public override void InterruptPuzzle()
        {
            base.InterruptPuzzle();
            ResolvePuzzle();
            Disable();
        }

        public override void ResumePuzzle()
        {
            base.ResumePuzzle();
            Enable();
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

        public void NextQuestion()
        {
            if (_isWaitingAnswer || !IsFocused || !IsInProgress) return;

            PrintQuestion();
            DisaplayAnswers();
        }

        private void NextQuestion(InputAction.CallbackContext ctx)
        {
            NextQuestion();
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
        }

        private void CorrectAnswer()
        {
            OnCorrectAnswer?.Invoke();
            PrintMessage(_correctAnswerMessage);
            _rewarder.SendReward(CurrentQuestion.PointsReward);

            _currentQuestionIndex++;
            if (_currentQuestionIndex > _currentQuizData.Questions.Count - 1)
                ResolvePuzzle();
        }

        private void WrongAnswer()
        {
            OnWrongAnswer?.Invoke();
            PrintMessage(_wrongAnswerMessage);
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
            PrintMessage(_quizCompletedMessage);
            DisableAnswersBox();
        }
    }
}