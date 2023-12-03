namespace VUDK.Features.Packages.DialogueSystem
{
    using UnityEngine;
    using VUDK.Features.Packages.DialogueSystem.Data;

    public class DSDialogue : MonoBehaviour
    {
        [SerializeField]
        private DSDialogueContainerData _dialogueContainer;
        [SerializeField]
        private DSDialogueGroupData _dialogueGroup;
        [SerializeField]
        private DSDialogueData _dialogue;

        [SerializeField]
        private bool _groupedDialogues;
        [SerializeField]
        private bool _startingDialoguesOnly;

        [SerializeField]
        private int _selectedDialogueGroupIndex;
        [SerializeField]
        private int _selectedDialogueIndex;
    }
}