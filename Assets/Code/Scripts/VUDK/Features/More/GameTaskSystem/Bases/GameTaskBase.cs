namespace VUDK.Features.More.GameTaskSystem.Bases
{
    using UnityEngine;
    using UnityEngine.Events;

    public abstract class GameTaskBase : MonoBehaviour
    {
        [Header("Task Settings")]
        [SerializeField]
        protected bool IsRepeatable;

        public bool IsSolved { get; protected set; }
        public bool IsInProgress { get; protected set; }
        public bool IsFocused { get; protected set; }

        [Header("Task Events")]
        [SerializeField]
        public UnityEvent OnTaskBegin;
        [SerializeField]
        public UnityEvent OnTaskResolved;
        [SerializeField]
        public UnityEvent OnTaskInterrupted;

        public virtual void BeginTask()
        {
            OnEnterFocus();

            if (!IsSolved || IsRepeatable)
            {
                OnTaskBegin?.Invoke();
                IsInProgress = true;
            }
        }

        public virtual void ResumeTask()
        {
            OnEnterFocus();
        }

        public virtual void InterruptTask()
        {
            OnExitFocus();
            OnTaskInterrupted?.Invoke();
        }

        public virtual void ResolveTask()
        {
            OnTaskResolved?.Invoke();
            IsSolved = true;
            IsInProgress = false;
            OnEnterFocus();
        }

        protected virtual void OnEnterFocus()
        {
            IsFocused = true;
            if (IsSolved && !IsRepeatable) OnEnterFocusIsSolved();
        }

        protected virtual void OnExitFocus()
        {
            IsFocused = false;
        }

        protected abstract void OnEnterFocusIsSolved();
    }
}