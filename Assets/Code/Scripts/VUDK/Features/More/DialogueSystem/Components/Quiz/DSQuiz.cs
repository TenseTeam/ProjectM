namespace VUDK.Features.More.DialogueSystem.Components.Quiz
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Events;

    public class DSQuiz : MonoBehaviour
    {
        [Header("Quiz Settings")]
        [SerializeField]
        private List<DSQuizAnswer> _quizAnswers;

        [Header("Quiz Events")]
        public UnityEvent OnIncorrectAnswer;

        private void OnEnable()
        {
            DSEvents.OnDMDialogue += CheckDialogue;
        }

        private void OnDisable()
        {
            DSEvents.OnDMDialogue -= CheckDialogue;
        }

        private void CheckDialogue(DSDialogueData dialogueData)
        {
            //if (_quizAnswers.Contains(dialogueData)) // TODO: Contains loop for checking answers
            //    OnCorrectAnswer?.Invoke();
            //else
            //    OnIncorrectAnswer?.Invoke();
        }
    }
}