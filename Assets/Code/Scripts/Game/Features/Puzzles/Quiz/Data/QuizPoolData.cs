namespace ProjectM.Features.Puzzles.Quiz.Data
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(QuizPoolData), menuName = "Project/Quizzes/Quiz Pool")]
    public class QuizPoolData : ScriptableObject
    {
        public List<QuizData> Pool;
    }
}