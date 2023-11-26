namespace ProjectM.Features.ExplorationSystem.Transition.Types
{
    using System;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectM.Features.ExplorationSystem.Nodes;
    using ProjectM.Managers;
    using ProjectM.Features.Player;

    public abstract class TransitionBase
    {
        public Action OnTransitionCompleted;
        protected TransitionContext Context { get; private set; }

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
