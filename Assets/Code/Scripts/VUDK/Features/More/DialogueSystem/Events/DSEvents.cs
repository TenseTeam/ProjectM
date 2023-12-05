namespace VUDK.Features.More.DialogueSystem.Events
{
    using System;
    using VUDK.Features.More.DialogueSystem.Data;

    public static class DSEvents
    {
        public static EventHandler<OnStartDialogueEventArgs> OnStartDialogue;
        public static Action OnEndDialogue;
        public static Action<int> OnSelectedChoice;
        public static Action OnInterrupt;
    }

    public class OnStartDialogueEventArgs : EventArgs
    {
        public DSDialogueContainerData DialogueContainerData;
        public DSDialogueData DialogueData;
        public bool RandomStart;
        public bool IsInstant;

        public OnStartDialogueEventArgs(DSDialogueContainerData dSDialogueContainerData, DSDialogueData dSDialogueData, bool randomStart, bool isInstant)
        {
            DialogueContainerData = dSDialogueContainerData;
            DialogueData = dSDialogueData;
            RandomStart = randomStart;
            IsInstant = isInstant;
        }
    }
}