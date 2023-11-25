namespace ProjectM.Patterns.Factories
{
    using VUDK.Patterns.StateMachine;
    using ProjectM.Features.ExplorationSystem.Transition;
    using ProjectM.Features.ExplorationSystem.Transition.Phases;
    using ProjectM.Features.ExplorationSystem.Transition.Phases.Keys;

    /// <summary>
    /// Factory responsible for creating game-related state-machine states and contexts.
    /// </summary>
    public static class MachineFactory
    {
        public static TransitionPhaseBase Create(TransitionStateKey key, StateMachine relatedStateMachine, TransitionContext context)
        {
            switch (key)
            {
                case TransitionStateKey.Start:
                    return new TransitionBegin(key, relatedStateMachine, context);
                case TransitionStateKey.Process:
                    return new TransitionProcess(key, relatedStateMachine, context);
                case TransitionStateKey.End:
                    return new TransitionEnd(key, relatedStateMachine, context);
            }

            return null;
        }

        public static TransitionContext Create()
        {
            return new TransitionContext();
        }
    }
}