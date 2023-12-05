namespace VUDK.Features.More.DialogueSystem.Events
{
    using System;
    using VUDK.Features.More.DialogueSystem.Data;

    public static class DSEvents
    {
        public static Action<DSDialogueContainerData, DSDialogueData, bool> OnStartDialogue;
        public static Action OnEndDialogue;
        public static Action<int> OnSelectedChoice;
        public static Action OnInterrupt;
    }
}