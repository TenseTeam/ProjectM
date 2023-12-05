namespace VUDK.Features.More.DialogueSystem
{
    using UnityEngine;
    using VUDK.Features.More.DialogueSystem.Data;

    public abstract class DSDialogueBase : MonoBehaviour
    {
        [SerializeField]
        protected DSDialogueContainerData DialogueContainer;
        [SerializeField]
        protected DSDialogueData Dialogue;
        [SerializeField]
        private DSDialogueGroupData _dialogueGroup;
        [SerializeField]
        protected bool RandomStartDialogue;

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