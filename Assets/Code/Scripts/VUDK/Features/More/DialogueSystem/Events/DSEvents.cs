namespace VUDK.Features.More.DialogueSystem.Events
{
    using System;
    using VUDK.Features.More.DialogueSystem.Data;

    public static class DSEvents
    {
        // Dialogue Handler Events
        public static EventHandler<OnStartDialogueEventArgs> DialogueStartHandler;
        public static EventHandler<OnDialogueChoiceEventArgs> DialogueChoiceHandler;
        public static Action DialogueInterruptHandler;

        // Dialogue Manager Events
        public static Action OnDMAnyNext;
        public static Action<DSDialogueData> OnDMNext;
        public static Action OnDMCompletedSentence;
        public static Action OnDMDisable;
        public static Action<DSDialogueData> OnDMInterrupt;
        public static Action<DSDialogueContainerData> OnDMStart;
        public static Action<DSDialogueContainerData> OnDMEnd;
        public static Action OnDMAnyStart;
        public static Action OnDMAnyEnd;
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

    public class OnDialogueChoiceEventArgs : EventArgs
    {
        public int ChoiceIndex;

        public OnDialogueChoiceEventArgs(int choiceIndex)
        {
            ChoiceIndex = choiceIndex;
        }
    }
}