namespace ProjectM.Features.Puzzles.Quiz.Data
{
    using System;
    using UnityEngine.Events;
    using VUDK.Features.More.DialogueSystem.Data;

    [Serializable]
    public struct QuizAnswerData
    {
        public DSDialogueData Answer;
        public int PointsReward;
        public UnityEvent OnAnswer;
    }
}
