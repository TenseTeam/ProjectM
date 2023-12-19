namespace ProjectM.Features.Puzzles.SeekPuzzle
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.More.GameTaskSystem;
    using ProjectM.Features.Puzzles.SeekPuzzle.Data;

    public class SeekPuzzle : GameTaskSaverBase
    {
        [Header("Targets Pool")]
        [SerializeField]
        private List<SeekGroupData> _seekGroups;

        public int RandomGroupIndex { get; private set; }

        public override void Init()
        {
            base.Init();
            RandomGroupIndex = Random.Range(0, _seekGroups.Count);

            foreach(SeekGroupData seekGroup in _seekGroups)
            {
                foreach (SeekFindable seekTarget in seekGroup.Targets)
                    seekTarget.Init(_seekGroups.IndexOf(seekGroup), this);
            }
        }

        public void Found(int seekGroupIndex, SeekFindable seekTarget)
        {
            if (RandomGroupIndex != seekGroupIndex) return;

            _seekGroups[RandomGroupIndex].FoundCount++;
            if (_seekGroups[RandomGroupIndex].HasFoundAll)
                ResolveTask();
        }
    }
}