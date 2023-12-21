namespace ProjectM.Features.Puzzles.SeekPuzzle
{
    using UnityEngine;
    using VUDK.Features.Main.TriggerSystem;
    using VUDK.Patterns.Initialization.Interfaces;

    public class SeekFindable : MonoBehaviour, IInit<int, SeekDialoguePuzzle>, ITrigger
    {
        [Header("Target Settings")]
        [SerializeField]
        private GameObject _targetGameObject;

        private int _seekGroupIndex;
        private SeekDialoguePuzzle _seekPuzzle;

        public void Init(int groupIndex, SeekDialoguePuzzle seekPuzzle)
        {
            _seekGroupIndex = groupIndex;
            _seekPuzzle = seekPuzzle;

            if(seekPuzzle.IsSolved || _seekPuzzle.GroupIndex != groupIndex)
                EnableTarget();
            else
                DisableTarget();
        }

        public bool Check()
        {
            return _seekPuzzle != null;
        }

        [ContextMenu("Trigger")]
        public void Trigger()
        {
            _seekPuzzle.Found(_seekGroupIndex);
            EnableTarget();
        }

        public void EnableTarget()
        {
            _targetGameObject.SetActive(true);
            gameObject.SetActive(false);
        }

        public void DisableTarget()
        {
            _targetGameObject.SetActive(false);
            gameObject.SetActive(true);
        }
    }
}