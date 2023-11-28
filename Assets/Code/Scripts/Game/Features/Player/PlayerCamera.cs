namespace ProjectM.Features.Player
{
    using ProjectM.Constants;
    using ProjectM.Features.ExplorationSystem;
    using ProjectM.Features.ExplorationSystem.Nodes;
    using VUDK.Extensions;
    using VUDK.Features.Main.Camera.CameraViews;
    using VUDK.Generic.Managers.Main;

    public class PlayerCamera : CameraFreeLook
    {
        private ExplorationManager _explorationManager;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnBeginTransition, OnBeginTransition);
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnEndTransition, OnEndTransition);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnBeginTransition, OnBeginTransition);
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnEndTransition, OnEndTransition);
        }

        public void Init(ExplorationManager eplorationManager)
        {
            _explorationManager = eplorationManager;
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
