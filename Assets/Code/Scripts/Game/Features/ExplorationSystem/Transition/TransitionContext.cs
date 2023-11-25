namespace ProjectM.Features.ExplorationSystem.Transition
{
    using ProjectM.Features.ExplorationSystem.Nodes;
    using ProjectM.Features.ExplorationSystem.Transition.Types;
    using ProjectM.Features.Player;
    using ProjectM.Managers;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Patterns.StateMachine;
    
    public class TransitionContext : StateMachineContext, ICastGameManager<GameManager>
    {
        // Managers
        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        public GameStats GameStats => MainManager.Ins.GameStats;
        public ExplorationManager ExplorationManager => GameManager.ExplorationManager;

        // Transition
        public NodeBase TargetNode => GameManager.ExplorationManager.CurrentTargetNode;
        public TransitionBase Transition => GameManager.ExplorationManager.CurrentTransition;

        // Player
        public PathExplorer PathExplorer => GameManager.ExplorationManager.PathExplorer;
        public PlayerCamera PlayerCamera { get; private set; }

        public TransitionContext() : base()
        {
            PlayerCamera = GameStats.PlayerCamera.TryGetComponent(out PlayerCamera playerCamera) ? playerCamera : null;
        }
    }
}
