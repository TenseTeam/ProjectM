namespace ProjectM.Features.Player
{
    using VUDK.Extensions;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Features.More.ExplorationSystem.Constants;
    using VUDK.Features.More.ExplorationSystem.Managers;
    using VUDK.Features.More.ExplorationSystem.Nodes;
    using VUDK.Features.Main.Camera.CameraViews;
    using VUDK.Features.Main.EventSystem;
    using ProjectM.Constants;

    public class PlayerCamera : CameraFreeLook, IInit<ExplorationManager>
    {
        private ExplorationManager _explorationManager;

        private void OnEnable()
        {
            EventManager.Ins.AddListener(GameEventKeys.OnEnterQuiz, Disable);
            EventManager.Ins.AddListener(GameEventKeys.OnExitQuiz, Enable);
            EventManager.Ins.AddListener(ExplorationEventKeys.OnBeginTransition, OnBeginTransition);
            EventManager.Ins.AddListener(ExplorationEventKeys.OnEndTransition, OnEndTransition);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(GameEventKeys.OnEnterQuiz, Disable);
            EventManager.Ins.RemoveListener(GameEventKeys.OnExitQuiz, Enable);
            EventManager.Ins.RemoveListener(ExplorationEventKeys.OnBeginTransition, OnBeginTransition);
            EventManager.Ins.AddListener(ExplorationEventKeys.OnEndTransition, OnEndTransition);
        }

        public void Init(ExplorationManager eplorationManager)
        {
            if (Check()) return;
            _explorationManager = eplorationManager;
        }

        public bool Check()
        {
            return _explorationManager != null;
        }

        /// <summary>
        /// Begins the transition.
        /// </summary>
        private void OnBeginTransition()
        {
            Disable();
        }

        /// <summary>
        /// Ends the transition.
        /// </summary>
        private void OnEndTransition()
        {
            TargetRotation = _explorationManager.CurrentTargetNode.NodeRotation.SignedEulerAngles();
            if (_explorationManager.CurrentTargetNode is not NodeView)
                Enable();
        }
    }
}
