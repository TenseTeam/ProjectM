namespace ProjectM.Features.Puzzles.SeekPuzzle
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Features.More.GameTaskSystem;
    using VUDK.Features.More.DialogueSystem.Events;
    using VUDK.Features.More.DialogueSystem.Data;
    using ProjectM.Features.Puzzles.SeekPuzzle.Data;

    public class SeekDialoguePuzzle : GameTaskSaverBase
    {
        [Header("Targets Pool")]
        [SerializeField]
        private List<SeekGroupData> _seekGroups;

        public int GroupIndex { get; private set; }

        private DSDialogueContainerData _loadedDialogue;
        private int _foundCount;

        public override void Init()
        {
            base.Init();

            GroupIndex = Random.Range(0, _seekGroups.Count);

            foreach(SeekGroupData seekGroup in _seekGroups)
            {
                foreach (SeekFindable seekTarget in seekGroup.Targets)
                    seekTarget.Init(_seekGroups.IndexOf(seekGroup), this);
            }
        }

        [ContextMenu("Begin Task")]
        public override void BeginTask()
        {
            base.BeginTask();
            _loadedDialogue = _seekGroups[GroupIndex].Dialogue.LoadAsset();

            if (!IsSolved || IsRepeatable)
                DSEventsHandler.StartDialogue(this, _loadedDialogue, null, true, false);
        }

        public void Found(int seekGroupIndex)
        {
            if (GroupIndex != seekGroupIndex) return;
            _foundCount++;

            if (HasFoundAll())
                ResolveTask();
        }

        public bool HasFoundAll()
        {
            return _foundCount == _seekGroups[GroupIndex].Targets.Count;
        }
    }
}