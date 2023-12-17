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
    using VUDK.Features.More.PuzzleSystem.Saving;
    using VUDK.Features.More.PuzzleSystem.Saving.SaveData;
    using ProjectM.Constants;
    using ProjectM.Features.Puzzles.Quiz.Data;
    using ProjectM.Features.Puzzles.Quiz.UI;
    using System;
    using ProjectM.Features.Puzzles.Quiz.Data.SaveData;
    using VUDK.Extensions;

    [RequireComponent(typeof(Rewarder))]
    public class QuizManager : SavedPuzzleBase
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

        [Header("Quiz Data")]
        [SerializeField]
        private List<QuizData> _quizDatas;

        private int _currentQuestionIndex;
        private int _currentQuizIndex;
        private bool _isWaitingAnswer;
        private Rewarder _rewarder;

        private QuizData CurrentQuizData => _quizDatas[_currentQuizIndex];
        private QuizQuestionData CurrentQuestion => CurrentQuizData.Questions[_currentQuestionIndex];
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
            EventManager.Ins.AddListener<int>(GameEventKeys.OnSelectQuizAnswer, ReceiveAnswer);
            InputsManager.Inputs.Interaction.Interact.canceled += DisplayQuestion;
        }

        private void OnDisable()
        {
            _closeButton.onClick.RemoveListener(InterruptPuzzle);
            EventManager.Ins.RemoveListener<int>(GameEventKeys.OnSelectQuizAnswer, ReceiveAnswer);
            InputsManager.Inputs.Interaction.Interact.canceled -= DisplayQuestion;
        }

        public override void Init(PuzzleSaveData data)
        {
            base.Init(data);

            if (IsInProgress)
            {
                QuizSaveData quizSaveData = data.AdditionalSaveValue as QuizSaveData;
                _currentQuizIndex = quizSaveData.QuizIndex;
                _currentQuestionIndex = quizSaveData.QuestionIndex;
            }
        }

        public void BeginQuiz()
        {
            if (!IsInProgress)
            {
                if (IsSolved && !IsRepeatable)
                {
                    DisplayCompletedQuiz();
                    return;
                }

                _currentQuizIndex = _quizDatas.IndexOf(_quizDatas.GetRandomElement());
                BeginPuzzle();
            }
            else
            {
                ResumePuzzle();
            }
        }

        public override void BeginPuzzle()
        {
            if (CurrentQuizData == null) return;
            base.BeginPuzzle();

            _currentQuestionIndex = 0;
            Enable();
            DisplayQuestion();
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

        public override void ResumePuzzle()
        {
            base.ResumePuzzle();
            DisplayQuestion();
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
            if (_currentQuestionIndex > CurrentQuizData.Questions.Count - 1)
                ResolvePuzzle();
            else
                SaveQuizState();
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
            PrintMessage(_correctAnswerMessage);

            if(!IsSolved)
                _rewarder.SendReward(CurrentQuestion.PointsReward);
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

        private void SaveQuizState()
        {
            SaveData.IsInProgress = IsInProgress;
            SaveData.AdditionalSaveValue = new QuizSaveData(_currentQuizIndex, _currentQuestionIndex);
            Save();
        }
    }
}