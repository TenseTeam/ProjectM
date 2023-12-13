namespace VUDK.Features.More.PuzzleSystem.Bases
{
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class PuzzleBase : MonoBehaviour
    {
        [Header("Puzzle Settings")]
        [SerializeField]
        protected bool IsRepeatable;

        public bool IsSolved { get; private set;  }
        public bool IsFocused { get; private set; }
        public bool IsInProgress { get; private set; }

        [Header("Puzzle Events")]
        [SerializeField]
        public UnityEvent OnPuzzleBegin;
        [SerializeField]
        public UnityEvent OnPuzzleResolved;

        public virtual void BeginPuzzle()
        {
            OnPuzzleBegin?.Invoke();
            IsFocused = true;
            IsInProgress = true;
        }

        public virtual void ResumePuzzle()
        {
            IsFocused = true;
        }

        public virtual void InterruptPuzzle()
        {
            IsFocused = false;
        }

        public virtual void ResolvePuzzle()
        {
            OnPuzzleResolved?.Invoke();
            IsSolved = true;
            IsFocused = false;
            IsInProgress = false;
        }
    }
}