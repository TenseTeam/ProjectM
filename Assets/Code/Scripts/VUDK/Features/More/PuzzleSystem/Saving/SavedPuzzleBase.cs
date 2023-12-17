namespace VUDK.Features.More.PuzzleSystem.Saving
{
    using System;
    using UnityEngine;
    using VUDK.Features.More.PuzzleSystem.Bases;
    using VUDK.Features.More.PuzzleSystem.Saving.SaveData;
    using VUDK.Patterns.Initialization.Interfaces;

    public class SavedPuzzleBase : PuzzleBase, IInit<PuzzleSaveData>
    {
        [Header("Saved Puzzle Settings")]
        [SerializeField]
        private int _hoursToReset = 24;

        public PuzzleSaveData SaveData { get; private set; }

        protected virtual void Awake()
        {
            SaveData = new PuzzleSaveData(false, false, DateTime.MinValue, null);
        }

        /// <summary>
        /// Initializes puzzle with save data.
        /// </summary>
        /// <param name="data"></param>
        public virtual void Init(PuzzleSaveData data)
        {
            SaveData = data;
            IsInProgress = SaveData.IsInProgress;
            IsSolved = SaveData.HasBeenEverSolved && !IsTimePassed();
        }

        public bool Check()
        {
            return SaveData != null;
        }

        private bool IsTimePassed()
        {
            return DateTime.Now > SaveData.LastCompletedTime.AddHours(_hoursToReset);
        }

        public override void BeginPuzzle()
        {
            base.BeginPuzzle();
            Save();
        }

        [ContextMenu("Resolve Puzzle")]
        public override void ResolvePuzzle()
        {
            base.ResolvePuzzle();
            SaveData.IsInProgress = IsInProgress;
            SaveData.HasBeenEverSolved = true;
            SaveData.LastCompletedTime = DateTime.Now;
            Save();
        }

        public void Save()
        {
            PuzzlesSaver.SavePuzzles();
        }
    }
}
