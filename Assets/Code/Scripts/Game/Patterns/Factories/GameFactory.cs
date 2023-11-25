namespace ProjectM.Patterns.Factories
{
    using VUDK.Features.Main.Camera.CameraModifiers;
    using ProjectM.Features.ExplorationSystem.Transition.Types.Keys;
    using ProjectM.Features.ExplorationSystem.Transition.Types;
    using VUDK.Generic.Serializable;
    using ProjectM.Features.ExplorationSystem.Transition;

    /// <summary>
    /// Factory responsible for creating game-related objects.
    /// </summary>
    public static class GameFactory
    {
        public static TransitionBase Create(TransitionContext context, TransitionType type, CameraFovChanger fovChanger, TimeDelayTask timeProcess)
        {
            switch (type)
            {
                case TransitionType.Instant:
                    return new TransitionInstant(context);
                case TransitionType.Linear:
                    return new TransitionLinear(context, timeProcess);
                case TransitionType.Fov:
                    return new TransitionFov(context, fovChanger, timeProcess);
            }

            return null;
        }
    }
}