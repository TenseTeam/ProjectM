namespace VUDK.Features.More.PuzzleSystem.Saving.SaveData
{
    using System.Collections.Generic;
    using VUDK.Features.Main.SaveSystem.Data;

    [System.Serializable]
    public class PuzzlesSaveData : SaveData
    {
        public List<PuzzleSaveData> PuzzleDatas;

        public PuzzlesSaveData()
        {
            PuzzleDatas = new List<PuzzleSaveData>();
        }

        public PuzzlesSaveData(SavedPuzzleBase[] puzzles)
        {
            PuzzleDatas = new List<PuzzleSaveData>();

            foreach (var puzzle in puzzles)
                PuzzleDatas.Add(puzzle.SaveData);
        }
    }
}