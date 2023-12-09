namespace VUDK.Features.More.PuzzleSystem.Bases
{
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class PuzzleBase : MonoBehaviour
    {
        [Header("Puzzle Settings")]
        [SerializeField]
        private bool _isRepeatable;

        public bool IsSolved { get; private set;  }

        [Header("Puzzle Events")]
        [SerializeField]
        public UnityEvent OnPuzzleBegin;
        [SerializeField]
        public UnityEvent OnPuzzleResolved;

        public virtual bool BeginPuzzle()
        {
            if (IsSolved && !_isRepeatable) return false;
            return true;
        }

        public virtual void ResolvePuzzle()
        {
            IsSolved = true;
        }
    }
}