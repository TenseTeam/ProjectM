namespace ProjectM.Features.Player
{
    using VUDK.Features.Packages.ExplorationSystem.Constants;
    using VUDK.Features.Packages.ExplorationSystem.Managers;
    using VUDK.Features.Packages.ExplorationSystem.Nodes;
    using VUDK.Features.Main.Camera.CameraViews;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Extensions;
    using VUDK.Patterns.DependencyInjection.Interfaces;

    public class PlayerCamera : CameraFreeLook, IInject<ExplorationManager>
    {
        private ExplorationManager _explorationManager;

        private void OnEnable()
        {
            EventManager.Ins.AddListener(ExplorationEventKeys.OnBeginTransition, OnBeginTransition);
            EventManager.Ins.AddListener(ExplorationEventKeys.OnEndTransition, OnEndTransition);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(ExplorationEventKeys.OnBeginTransition, OnBeginTransition);
            EventManager.Ins.AddListener(ExplorationEventKeys.OnEndTransition, OnEndTransition);
        }

        public void Inject(ExplorationManager eplorationManager)
        {
            if (Check()) return;
            _explorationManager = eplorationManager;
        }

        public bool Check()
        {
            return _explorationManager != null;
        }

        private void OnBeginTransition()
        {
            Disable();
        }

        private void OnEndTransition()
        {
            TargetRotation = _explorationManager.CurrentTargetNode.NodeRotation.SignedEulerAngles();
            if (_explorationManager.CurrentTargetNode is not NodeObservation)
                Enable();
        }
    }
}
