namespace ProjectM.Features.Puzzles.Quiz.Data
{
    using UnityEngine;
    using ProjectM.Features.Quiz.Data.Structs;

    [System.Serializable]
    public struct QuizQuestionData
    {
        public string QuestionText;
        [Min(0)]
        public int PointsReward;
        public QuizAnswerData[] Answers;
    }
}