﻿namespace VUDK.Features.Packages.ExplorationSystem.Transition.Types
{
    using VUDK.Features.Main.Camera.CameraModifiers;
    using VUDK.Generic.Serializable;

    public class TransitionFov : TransitionLinear
    {
        private CameraFovChanger _fovChanger;
        private bool _hasReverted;

        public TransitionFov(TransitionContext context, CameraFovChanger fovChanger, TimeDelayTask timeProcess) : base(context, timeProcess)
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