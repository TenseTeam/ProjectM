namespace ProjectM.Features.Puzzles.Quiz.Data
{
    using ProjectM.Features.Quiz.Data.Structs;

    [System.Serializable]
    public struct QuizQuestionData
    {
        public string QuestionText;
        public QuizAnswerData[] Answers;
    }
}