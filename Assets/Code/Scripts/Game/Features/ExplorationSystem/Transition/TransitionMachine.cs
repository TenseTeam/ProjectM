namespace ProjectM.Features.ExplorationSystem.Transition
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.Main.Camera.CameraModifiers;
    using ProjectM.Features.ExplorationSystem.Transition.Phases;
    using ProjectM.Features.ExplorationSystem.Transition.Phases.Keys;
    using ProjectM.Features.ExplorationSystem.Transition.Types;
    using ProjectM.Features.ExplorationSystem.Transition.Types.Keys;
    using ProjectM.Patterns.Factories;

    public class TransitionMachine : StateMachine
    {
        [Header("Transition Settings")]
        [SerializeField]
        private TransitionType _transitionType;

        [SerializeField]
        private TimeDelayTask _timeProcess;
        [SerializeField]
        private CameraFovChanger _fovChanger;

        protected override void Start()
        {
            base.Start();
            Init();
        }

        public override void Init()
        {
            TransitionBase transition = GameFactory.Create(_transitionType, _fovChanger, _timeProcess);
            TransitionContext context = MachineFactory.Create(transition);

            TransitionBegin beginPhase = MachineFactory.Create(TransitionStateKey.Start, this, context) as TransitionBegin;
            TransitionProcess processPhase = MachineFactory.Create(TransitionStateKey.Process, this, context) as TransitionProcess;
            TransitionEnd endPhase = MachineFactory.Create(TransitionStateKey.End, this, context) as TransitionEnd;

            AddState(TransitionStateKey.Start, beginPhase);
            AddState(TransitionStateKey.Process, processPhase);
            AddState(TransitionStateKey.End, endPhase);
        }
    }
}
