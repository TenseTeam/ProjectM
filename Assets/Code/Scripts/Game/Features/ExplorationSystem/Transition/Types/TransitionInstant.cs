namespace ProjectM.Features.ExplorationSystem.Transition.Types
{
    using UnityEngine;
    using ProjectM.Features.ExplorationSystem.Nodes;

    public class TransitionInstant : TransitionBase
    {
        public TransitionInstant() : base()
        {
        }

        public override void Begin()
        {
        }

        public override void Process()
        {
            PathExplorer.transform.position = TargetNode.NodePosition;
            OnTransitionCompleted?.Invoke();
        }

        public override void End()
        {
        }
    }
}
