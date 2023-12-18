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

        public virtual void BeginTask()
        {
            if(!IsSolved || IsRepeatable)
                OnTaskBegin?.Invoke();
            OnEnterFocus();
            IsInProgress = true;
        }

        public virtual void ResumeTask()
        {
            OnEnterFocus();
        }

        public virtual void InterruptTask()
        {
            OnExitFocus();
        }

        public virtual void ResolveTask()
        {
            OnTaskResolved?.Invoke();
            IsSolved = true;
            IsInProgress = false;
            OnEnterFocus();
        }

        public virtual void OnEnterFocus()
        {
            IsFocused = true;
        }

        public virtual void OnExitFocus()
        {
            IsFocused = false;
        }
    }
}