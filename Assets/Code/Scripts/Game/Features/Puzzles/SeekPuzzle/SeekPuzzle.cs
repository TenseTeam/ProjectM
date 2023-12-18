namespace ProjectM.Features.Puzzles.SeekPuzzle
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.More.GameTaskSystem;

    public class SeekPuzzle : GameTaskSaverBase
    {
        [Header("Targets Pool")]
        [SerializeField]
        private List<SeekTarget> _seekTargets;


    }
}