namespace VUDK.Features.Packages.ExplorationSystem.Factory
{
    using VUDK.Features.Main.Camera.CameraModifiers;
    using VUDK.Features.Packages.ExplorationSystem.Transition;
    using VUDK.Features.Packages.ExplorationSystem.Transition.Phases;
    using VUDK.Features.Packages.ExplorationSystem.Transition.Phases.Keys;
    using VUDK.Features.Packages.ExplorationSystem.Transition.Types;
    using VUDK.Patterns.StateMachine;
    using VUDK.Generic.Serializable;
    using VUDK.Features.Packages.ExplorationSystem.Managers;

    public static class ExplorationFactory
    {
        public static TransitionInstant Create(TransitionContext context)
        {
            return new TransitionInstant(context);
        }

        public static TransitionLinear Create(TransitionContext context, TimeDelayTask timeProcess)
        {
            return new TransitionLinear(context, timeProcess);
        }

        public static TransitionFov Create(TransitionContext context, CameraFovChanger fovChanger, TimeDelayTask timeProcess)
        {
            return new TransitionFov(context, fovChanger, timeProcess);
        }

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

        public static TransitionContext Create(ExplorationManager explorationManager)
        {
            return new TransitionContext(explorationManager);
        }
    }
}