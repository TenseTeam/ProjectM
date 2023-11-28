namespace ProjectM.Features.ExplorationSystem.Transition.Types
{
    public class TransitionInstant : TransitionBase
    {
        public TransitionInstant(TransitionContext context) : base(context)
        {
        }

        public override void Begin()
        {
            Context.PathExplorer.transform.SetPositionAndRotation(Context.TargetNode.NodePosition, Context.TargetNode.NodeRotation);
        }

        public override void Process()
        {
            OnTransitionCompletedHandler();
        }

        public override void End()
        {
        }
    }
}
