namespace ProjectM.Features.ExplorationSystem.Transition.Types
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using ProjectM.Features.Player;

    public class TransitionLinear : TransitionBase
    {
        protected TimeDelayTask TimeProcess;
        protected Vector3 StartPosition;
        protected Quaternion StartRotation;

        public TransitionLinear(TimeDelayTask timeProcess) : base()
        {
            TimeProcess = timeProcess;
        }

        public override void Begin()
        {
            StartPosition = PathExplorer.transform.position;
            StartRotation = PlayerCamera.transform.rotation;
            TimeProcess.Start();
            TimeProcess.OnTaskCompleted += OnTransitionCompletedHandler;
        }

        public override void Process()
        {
            if (!TimeProcess.Process()) return;

            PathExplorer.transform.position = Vector3.Lerp(StartPosition, TargetNode.NodePosition, TimeProcess.ElapsedPercentPrecise);
            PlayerCamera.transform.rotation = Quaternion.Lerp(StartRotation, TargetNode.NodeRotation, TimeProcess.ElapsedPercentPrecise);
        }

        public override void End()
        {
            PlayerCamera.SetRotation(TargetNode.NodeRotation);
            TimeProcess.OnTaskCompleted -= OnTransitionCompletedHandler;
        }
    }
}