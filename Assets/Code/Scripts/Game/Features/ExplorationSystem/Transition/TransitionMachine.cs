namespace ProjectM.Features.ExplorationSystem.Transition
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.Main.Camera.CameraModifiers;
    using ProjectM.Patterns.Factories;
    using ProjectM.Features.ExplorationSystem.Transition.Phases;
    using ProjectM.Features.ExplorationSystem.Transition.Types.Keys;
    using ProjectM.Features.ExplorationSystem.Transition.Phases.Keys;

    public class TransitionMachine : StateMachine
    {
        [Header("Transition Settings")]
        [SerializeField]
        private TransitionType _transitionType;

        [SerializeField]
        private TimeDelayTask _timeProcess;
        [SerializeField]
        private CameraFovChanger _fovChanger;

        private TransitionContext _context;

        protected override void Start()
        {
            base.Start();
            Init();
            SetTransition(_transitionType);
        }

        public override void Init()
        {
            _context = MachineFactory.Create();

            TransitionBegin beginPhase = MachineFactory.Create(TransitionStateKey.Start, this, _context) as TransitionBegin;
            TransitionProcess processPhase = MachineFactory.Create(TransitionStateKey.Process, this, _context) as TransitionProcess;
            TransitionEnd endPhase = MachineFactory.Create(TransitionStateKey.End, this, _context) as TransitionEnd;

            AddState(TransitionStateKey.Start, beginPhase);
            AddState(TransitionStateKey.Process, processPhase);
            AddState(TransitionStateKey.End, endPhase);
        }

        public void SetTransition(TransitionType transitionType)
        {
            _context.ExplorationManager.SetTransition(GameFactory.Create(_context, transitionType, _fovChanger, _timeProcess));
        }
    }
}
