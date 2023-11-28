namespace ProjectM.Features.ExplorationSystem.Transition
{
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.StateMachine;
    using ProjectM.Features.ExplorationSystem.Transition.Types;
    using ProjectM.Features.ExplorationSystem.Nodes;
    
    public class TransitionContext : StateMachineContext
    {
        public ExplorationManager ExplorationManager { get; private set; }

        public GameStats GameStats => MainManager.Ins.GameStats;
        public NodeBase TargetNode => ExplorationManager.CurrentTargetNode;
        public NodeBase PreviousNode => ExplorationManager.PreviousTargetNode;
        public TransitionBase Transition => ExplorationManager.CurrentTransition;
        public PathExplorer PathExplorer => ExplorationManager.PathExplorer;

        public TransitionContext(ExplorationManager explorationManager) : base()
        {
            ExplorationManager = explorationManager;
        }
    }
}
