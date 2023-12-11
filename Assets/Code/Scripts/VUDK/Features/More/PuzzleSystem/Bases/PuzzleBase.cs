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
        public bool IsInProgress { get; private set; }

        [Header("Puzzle Events")]
        [SerializeField]
        public UnityEvent OnPuzzleBegin;
        [SerializeField]
        public UnityEvent OnPuzzleResolved;

        public virtual void BeginPuzzle()
        {
            ResolvePuzzle();
        }

        public virtual void ResumePuzzle()
        {
            IsInProgress = true;
        }

        public virtual void InterruptPuzzle()
        {
            IsInProgress = false;
        }

        public virtual void ResolvePuzzle()
        {
            IsSolved = true;
            IsInProgress = false;
        }
    }
}