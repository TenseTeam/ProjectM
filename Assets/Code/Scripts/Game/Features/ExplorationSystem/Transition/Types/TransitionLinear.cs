namespace ProjectM.Features.ExplorationSystem.Transition.Types
{
    using UnityEngine;
    using VUDK.Generic.Serializable;

    public class TransitionLinear : TransitionBase
    {
        private TimeDelayTask _timeProcess;
        private Vector3 _startPosition;

        public TransitionLinear(TimeDelayTask timeProcess) : base()
        {
            _timeProcess = timeProcess;
        }

        public override void Begin()
        {
            _startPosition = PathExplorer.transform.position;
            _timeProcess.Start();
            _timeProcess.OnTaskCompleted += OnTransitionCompletedHandler;
        }

        public override void Process()
        {
            if (!_timeProcess.Process()) return;

            PathExplorer.transform.position = Vector3.Lerp(_startPosition, TargetNode.NodePosition, _timeProcess.ElapsedPercentPrecise);
        }

        public override void End()
        {
            _timeProcess.OnTaskCompleted -= OnTransitionCompletedHandler;
        }
    }
}