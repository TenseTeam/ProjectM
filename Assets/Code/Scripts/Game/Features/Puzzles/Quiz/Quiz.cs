namespace ProjectM.Features.Puzzles.Quiz
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using VUDK.Features.Main.PointsSystem.Rewards;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Events;
    using VUDK.Features.More.PuzzleSystem.Bases;
    using ProjectM.Features.Puzzles.Quiz.Data;

    [RequireComponent(typeof(Rewarder))]
    public class Quiz : PuzzleBase
    {
        [Header("Quiz Settings")]
        [SerializeField]
        private DSDialogueContainerData _quizDialogueData;
        [SerializeField]
        private List<QuizAnswerData> _quizAnswers;
        [SerializeField]
        private bool _isInstant;

        [Header("Quiz Events")]
        public UnityEvent OnAnyCorrectAnswer;
        public UnityEvent OnIncorrectAnswer;

        private Rewarder _rewarder;
        private DSDialogueData _lastDialogue;

        private void Awake()
        {
            TryGetComponent(out _rewarder);
        }

        private void OnEnable()
        {
            DSEvents.OnDMNext += CheckDialogue;
            DSEvents.OnDMEnd += CompleteQuiz;
        }

        private void OnDisable()
        {
            DSEvents.OnDMNext -= CheckDialogue;
            DSEvents.OnDMEnd -= CompleteQuiz;
            DSEvents.OnDMInterrupt -= RegisterLastDialogue;
        }

        public override void BeginPuzzle()
        {
            base.BeginPuzzle();
            if(IsSolved && !IsRepeatable) return;
            DSEvents.OnDMInterrupt += RegisterLastDialogue;
            if (!IsInProgress)
                StartQuiz(null, true);
            else
                ResumePuzzle();
            
            _lastDialogue = null;
        }

        public override void ResumePuzzle()
        {
            base.ResumePuzzle();
            StartQuiz(_lastDialogue, false);
        }

        public override void InterruptPuzzle()
        {
            base.InterruptPuzzle();
            DSEvents.DialogueInterruptHandler?.Invoke();
            DSEvents.OnDMInterrupt -= RegisterLastDialogue;
        }

        private void StartQuiz(DSDialogueData fromDialogue, bool randomStartDialogue)
        {
            DSEventsHandler.StartDialogue(this, _quizDialogueData, fromDialogue, randomStartDialogue, _isInstant);
        }

        private void CompleteQuiz(DSDialogueContainerData container)
        {
            if (container != _quizDialogueData) return;

            ResolvePuzzle();
            DSEvents.OnDMInterrupt -= RegisterLastDialogue;
        }

        private void RegisterLastDialogue(DSDialogueData dialogue)
        {
            _lastDialogue = dialogue;
        }

        private void CheckDialogue(DSDialogueData dialogueData)
        {
            if (TryGetAnswer(dialogueData, out QuizAnswerData answerData))
            {
                _rewarder.SendReward(answerData.PointsReward);
                OnAnyCorrectAnswer?.Invoke();
                answerData.OnAnswer?.Invoke();
            }
            else
                OnIncorrectAnswer?.Invoke();
        }

        private bool TryGetAnswer(DSDialogueData dialogueData, out QuizAnswerData answerData)
        {
            foreach(QuizAnswerData quizAnswer in _quizAnswers)
            {
                if (quizAnswer.Answer == dialogueData)
                {
                    answerData = quizAnswer;
                    return true;
                }
            }

            answerData = default;
            return false;
        }
    }
}