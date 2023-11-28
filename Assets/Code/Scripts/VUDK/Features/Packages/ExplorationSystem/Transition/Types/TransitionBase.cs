namespace VUDK.Features.Packages.ExplorationSystem.Transition.Types
{
    using System;

    public abstract class TransitionBase
    {
        protected TransitionContext Context { get; private set; }

        public Action OnTransitionCompleted;

        public TransitionBase(TransitionContext context)
        {
            Context = context;
        }

        public virtual void Begin()
        {
        }

        public virtual void Process()
        {
        }

        public virtual void End()
        {
        }

        public virtual void OnTransitionCompletedHandler()
        {
            OnTransitionCompleted?.Invoke();
        }
    }
}
