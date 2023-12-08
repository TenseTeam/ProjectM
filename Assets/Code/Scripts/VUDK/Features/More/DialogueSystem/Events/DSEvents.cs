namespace VUDK.Features.More.DialogueSystem.Events
{
    using System;
    using VUDK.Features.More.DialogueSystem.Data;

    public static class DSEvents
    {
        public static EventHandler<OnStartDialogueEventArgs> DialogueStartHandler;
        public static EventHandler<OnDialogueChoiceEventArgs> DialogueChoiceHandler;
        public static Action DialogueInterruptHandler;

        public static Action OnDMNext;
        public static Action OnDMCompletedSentence;
        public static Action OnDMStart;
        public static Action OnDMEnd;
        public static Action OnDMDisable;
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