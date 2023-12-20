namespace ProjectM.Features.Puzzles.SeekPuzzle.Data
{
    using System.Collections.Generic;

    [System.Serializable]
    public class SeekGroupData
    {
        public List<SeekFindable> Targets;

        public int FoundCount { get; set; }
        public bool HasFoundAll => FoundCount >= Targets.Count;
    }
}