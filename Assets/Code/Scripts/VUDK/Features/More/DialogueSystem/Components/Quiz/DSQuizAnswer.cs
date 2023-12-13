namespace VUDK.Features.More.DialogueSystem.Components.Quiz
{
    using System;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Events;

    [Serializable]
    public class DSQuizAnswer : MonoBehaviour
    {
        [SerializeField]
        private DSDialogueData _answer;
        [SerializeField]
        private UnityEvent _onCorrectAnswer;
    }
}
