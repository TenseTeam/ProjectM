namespace ProjectM.Features.ExplorationSystem.Transition.Types
{
    using UnityEngine;
    using VUDK.Features.Main.Camera.CameraModifiers;
    using VUDK.Generic.Serializable;

    public class TransitionFov : TransitionLinear
    {
        private TimeDelayTask _timeProcess;
        private CameraFovChanger _fovChanger;
        private bool _hasReverted;

        public TransitionFov(CameraFovChanger fovChanger, TimeDelayTask timeProcess) : base(timeProcess)
        {
            _fovChanger = fovChanger;
            _fovChanger.TimeProcess.ChangeDuration(TimeProcess.Duration / 2f);
        }

        public override void Begin()
        {
            base.Begin();
            _fovChanger.Change();
        }

        public override void Process()
        {
            base.Process();
            ////if (!TimeProcess.Process()) return;

            if (_fovChanger.TimeProcess.IsCompleted && !_hasReverted)
            {
                _hasReverted = true;
                _fovChanger.Revert();
            }
        }

        public override void End()
        {
            base.End();
            _hasReverted = false;
            _fovChanger.TimeProcess.Reset();
        }
    }
}