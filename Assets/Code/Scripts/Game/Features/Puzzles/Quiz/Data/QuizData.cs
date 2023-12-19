﻿namespace ProjectM.Features.Puzzles.Quiz.Data
{
    using System.Collections.Generic;
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(QuizData), menuName = "Project/Quiz")]
    public class QuizData : ScriptableObject
    {
        public List<QuizQuestionData> Questions;
    }
}