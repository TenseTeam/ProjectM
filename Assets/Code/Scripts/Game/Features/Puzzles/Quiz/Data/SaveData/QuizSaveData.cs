namespace ProjectM.Features.Puzzles.Quiz.Data.SaveData
{
    using System;
    using VUDK.Features.Main.SaveSystem.Data;

    [Serializable]
    public class QuizSaveData : SaveData
    {
        public int QuizIndex;
        public int QuestionIndex;

        public QuizSaveData()
        {
        }

        public QuizSaveData(int quizIndex, int questionIndex)
        {
            QuizIndex = quizIndex;
            QuestionIndex = questionIndex;
        }
    }
}