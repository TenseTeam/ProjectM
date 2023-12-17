namespace VUDK.Features.More.PuzzleSystem.Saving.SaveData
{
    using System;
    using VUDK.Features.Main.SaveSystem.Data;

    [System.Serializable]
    public class PuzzleSaveData
    {
        public bool IsInProgress;
        public bool HasBeenEverSolved;
        public DateTime LastCompletedTime;
        public SaveData AdditionalSaveValue;

        public PuzzleSaveData(bool isInProgress, bool hasBeenEverSolved, DateTime dateTime, SaveData additionalSaveValue)
        {
            IsInProgress = isInProgress;
            LastCompletedTime = dateTime;
            HasBeenEverSolved = hasBeenEverSolved;
            AdditionalSaveValue = additionalSaveValue;
        }
    }
}