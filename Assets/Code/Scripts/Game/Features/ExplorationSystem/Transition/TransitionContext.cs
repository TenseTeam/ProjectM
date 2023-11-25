namespace ProjectM.Features.ExplorationSystem.Transition
{
    using ProjectM.Features.ExplorationSystem.Nodes;
    using ProjectM.Features.ExplorationSystem.Transition.Types;
    using ProjectM.Managers;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Patterns.StateMachine;
    
    public class TransitionContext : StateMachineContext, ICastGameManager<GameManager>
    {
        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        public PathExplorer PathExplorer => GameManager.ExplorationManager.PathExplorer;
        public ExplorationManager ExplorationManager => GameManager.ExplorationManager;
        public NodeBase CurrentTargetNode => GameManager.ExplorationManager.CurrentTargetNode;
        public TransitionBase Transition => GameManager.ExplorationManager.CurrentTransition;

        public TransitionContext() : base()
        {
        }
    }
}
