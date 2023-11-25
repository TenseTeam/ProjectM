namespace ProjectM.Features.ExplorationSystem.Transition.Types
{
    using UnityEngine;
    using VUDK.Features.Main.Camera.CameraModifiers;
    using VUDK.Generic.Serializable;

    public class TransitionFov : TransitionBase
    {
        private TimeDelayTask _timeProcess;
        private CameraFovChanger _fovChanger;
        private Vector3 _startPosition;
        private bool _hasReverted;

        public TransitionFov(CameraFovChanger fovChanger, TimeDelayTask timeProcess) : base()
        {
            _fovChanger = fovChanger;
            _timeProcess = timeProcess;
            _fovChanger.TimeProcess.ChangeDuration(_timeProcess.Duration / 2f);
        }

        public override void Begin()
        {
            _startPosition = PathExplorer.transform.position;
            _timeProcess.Start();
            _timeProcess.OnTaskCompleted += OnTransitionCompletedHandler;
            _fovChanger.Change();
        }

        public override void Process()
        {
            if (!_timeProcess.Process()) return;

            PathExplorer.transform.position = Vector3.Lerp(_startPosition, TargetNode.NodePosition, _timeProcess.ElapsedPercentPrecise);

            if (_timeProcess.ElapsedPercentPrecise >= .5f && !_hasReverted)
            {
                _hasReverted = true;
                _fovChanger.Revert();
            }
        }

        public override void End()
        {
            _timeProcess.OnTaskCompleted -= OnTransitionCompletedHandler;
            _hasReverted = false;
        }
    }
}