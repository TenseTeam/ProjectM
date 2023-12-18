namespace ProjectM.Features.Puzzles.Quiz.Data.SaveData
{
    using VUDK.Features.More.GameTaskSystem.SaveData;

    [System.Serializable]
    public class QuizSaveValue : TaskSaveValue
    {
        public int QuizIndex;
        public int QuestionIndex;

        public QuizSaveValue() : base() { }
    }
}