namespace VUDK.Features.Packages.DialogueSystem.Data
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.Packages.DialogueSystem.Enums;

    public class DSDialogueData : ScriptableObject
    {
        public string DialogueName;
        [TextArea(3, 10)]
        public string DialogueText;
        public List<DSDialogueChoiceData> Choices;
        public DSDialogueType DialogueType;
        public bool IsStartDialogue;

        public void Init(string dialogueName, string dialogueText, List<DSDialogueChoiceData> choices, DSDialogueType dialogueType, bool isStartDialogue = false)
        {
            DialogueName = dialogueName;
            DialogueText = dialogueText;
            Choices = choices;
            DialogueType = dialogueType;
            IsStartDialogue = isStartDialogue;
        }
    }
}
