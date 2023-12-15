namespace ProjectM.Features.Puzzles.DialogueQuiz.Data
{
    using System;
    using UnityEngine.Events;
    using VUDK.Features.More.DialogueSystem.Data;

    [Serializable]
    public struct DialogueQuizAnswerData
    {
        public DSDialogueData Answer;
        public int PointsReward;
        public UnityEvent OnAnswer;
    }
}
