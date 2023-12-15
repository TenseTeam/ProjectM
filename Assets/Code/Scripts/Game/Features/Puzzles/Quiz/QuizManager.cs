namespace ProjectM.Puzzles.Quiz
{
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.More.PuzzleSystem.Bases;
    using ProjectM.Constants;
    using ProjectM.Features.Puzzles.Quiz.Data;
    using ProjectM.Features.Puzzles.Quiz.UI;
    using VUDK.Features.Main.InputSystem;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;

    public class QuizManager : PuzzleBase
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

        private QuizQuestionData CurrentQuestion => _currentQuizData.Questions[_currentQuestionIndex];
        private int MaxChoices => _answersButtons.Count;

        private void Awake()
        {
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
                _currentQuizData = quizData;
                BeginPuzzle();
                return;
            }

            Enable();
        }

        public override void BeginPuzzle()
        {
            if (_currentQuizData == null) return;
            if (IsSolved && !IsRepeatable) return;
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
            Disable();
        }

        public void Enable()
        {
            _quizPanel.gameObject.SetActive(true);
        }

        public void Disable()
        {
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
            Debug.Log("NextQuestion Input");
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
            PrintMessage(_correctAnswerMessage);
            _currentQuestionIndex++;

            if(_currentQuestionIndex > _currentQuizData.Questions.Count - 1)
                ResolvePuzzle();
        }

        private void WrongAnswer()
        {
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
    }
}