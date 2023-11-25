namespace ProjectM.Patterns.Factories
{
    using VUDK.Features.Main.Camera.CameraModifiers;
    using ProjectM.Features.ExplorationSystem.Transition.Types.Keys;
    using ProjectM.Features.ExplorationSystem.Transition.Types;
    using VUDK.Generic.Serializable;

    /// <summary>
    /// Factory responsible for creating game-related objects.
    /// </summary>
    public static class GameFactory
    {
        public static TransitionBase Create(TransitionType type, CameraFovChanger fovChanger, TimeDelayTask timeProcess)
        {
            switch (type)
            {
                case TransitionType.Instant:
                    return new TransitionInstant();
                case TransitionType.Linear:
                    return new TransitionLinear(timeProcess);
                case TransitionType.Fov:
                    return new TransitionFov(fovChanger, timeProcess);
            }

            return null;
        }
    }
}