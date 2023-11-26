namespace ProjectM.Features.ExplorationSystem.Transition.Types
{
    public class TransitionInstant : TransitionBase
    {
        public TransitionInstant(TransitionContext context) : base(context)
        {
        }

        public override void Begin()
        {
            Context.PlayerCamera.SetRotation(Context.TargetNode.NodeRotation);
        }

        public override void Process()
        {
            Context.PathExplorer.transform.position = Context.TargetNode.NodePosition;
            OnTransitionCompletedHandler();
        }

        public override void End()
        {
        }
    }
}
