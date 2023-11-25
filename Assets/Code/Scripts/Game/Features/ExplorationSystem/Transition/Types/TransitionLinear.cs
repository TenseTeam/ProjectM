namespace ProjectM.Features.ExplorationSystem.Transition.Types
{
    using UnityEngine;
    using VUDK.Generic.Serializable;

    public class TransitionLinear : TransitionBase
    {
        protected TimeDelayTask TimeProcess;
        protected Vector3 StartPosition;
        protected Quaternion StartRotation;

        public TransitionLinear(TransitionContext context, TimeDelayTask timeProcess) : base(context)
        {
            TimeProcess = timeProcess;
        }

        public override void Begin()
        {
            StartPosition = Context.PathExplorer.transform.position;
            StartRotation = Context.PlayerCamera.transform.rotation;
            TimeProcess.Start();
            TimeProcess.OnTaskCompleted += OnTransitionCompletedHandler;
        }

        public override void Process()
        {
            if (!TimeProcess.Process()) return;

            Context.PathExplorer.transform.position = Vector3.Lerp(StartPosition, Context.TargetNode.NodePosition, TimeProcess.ElapsedPercentPrecise);
            Context.PlayerCamera.transform.localRotation = Quaternion.Lerp(StartRotation, Context.TargetNode.NodeRotation, TimeProcess.ElapsedPercentPrecise);
        }

        public override void End()
        {
            Context.PlayerCamera.SetRotation(Context.TargetNode.NodeRotation);
            TimeProcess.OnTaskCompleted -= OnTransitionCompletedHandler;
        }
    }
}