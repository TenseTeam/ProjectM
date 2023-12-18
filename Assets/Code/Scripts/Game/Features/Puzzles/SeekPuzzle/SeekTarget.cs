namespace ProjectM.Features.Puzzles.SeekPuzzle
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Patterns.Initialization.Interfaces;

    [RequireComponent(typeof(Button))]
    public class SeekTarget : MonoBehaviour, IInit<SeekPuzzle>
    {
        private SeekPuzzle _seekPuzzle;
        private Button _button;

        private void Awake()
        {
            TryGetComponent(out _button);
        }

        public void Init(SeekPuzzle seekPuzzle)
        {
            _seekPuzzle = seekPuzzle;
        }

        public bool Check()
        {
            return _seekPuzzle != null;
        }
    }
}