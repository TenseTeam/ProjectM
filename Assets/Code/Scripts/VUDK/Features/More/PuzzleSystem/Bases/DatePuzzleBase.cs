namespace VUDK.Features.More.PuzzleSystem.Bases
{

    using UnityEngine;
    using VUDK.Features.More.DateTaskSystem;

    [RequireComponent(typeof(DateTask))]
    public class DatePuzzleBase : PuzzleBase
    {
        private DateTask _dateTask;

        protected virtual void Awake()
        {
            TryGetComponent(out _dateTask);
        }

        protected virtual void Start()
        {
            _dateTask.Init();
            IsSolved = _dateTask.HasCompleted;
        }

        public override void ResolvePuzzle()
        {
            if(!IsSolved)
                _dateTask.CompleteTask();

            base.ResolvePuzzle();
        }
    }
}
