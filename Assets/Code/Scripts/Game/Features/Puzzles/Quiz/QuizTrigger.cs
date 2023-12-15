namespace ProjectM.Features.Puzzles.Quiz
{
    using UnityEngine;
    using VUDK.Features.Main.TriggerSystem;
    using VUDK.Features.Main.EventSystem;
    using ProjectM.Constants;
    using ProjectM.Features.Puzzles.Quiz.Data;

    public class QuizTrigger : MonoBehaviour, ITrigger
    {
        [Header("Quiz Settings")]
        [SerializeField]
        private QuizData _quizData;

        [ContextMenu("Trigger")]
        public void Trigger()
        {
            EventManager.Ins.TriggerEvent(GameEventKeys.OnQuizTrigger, _quizData);
        }
    }
}