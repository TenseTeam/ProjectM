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
    using ProjectM.Features.ExplorationSystem.Transition.Types;

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

        private TransitionType _defaultTransition;
        private TransitionType _currentTransition;
        private TransitionInstant _transitionInstant;
        private TransitionLinear _transitionLinear;
        private TransitionFov _transitionFov;

        protected override void Start()
        {
            base.Start();
            Init();
            _defaultTransition = _transitionType;
            ChangeTransitionType(_defaultTransition);
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

        public void DefaultTransition()
        {
            ChangeTransitionType(_defaultTransition);
        }

        public void ChangeTransitionType(TransitionType transitionType)
        {
            if(_currentTransition == transitionType) return;

            _currentTransition = transitionType;
            switch(transitionType)
            {
                case TransitionType.Instant:
                    if(_transitionInstant == null)
                        _transitionInstant = GameFactory.Create(_context, transitionType, _fovChanger, _timeProcess) as TransitionInstant;
                    _context.ExplorationManager.SetTransition(_transitionInstant);
                    break;

                case TransitionType.Linear:
                    if(_transitionLinear == null)
                        _transitionLinear = GameFactory.Create(_context, transitionType, _fovChanger, _timeProcess) as TransitionLinear;
                    _context.ExplorationManager.SetTransition(_transitionLinear);
                    break;

                case TransitionType.Fov:
                    if(_transitionFov == null)
                        _transitionFov = GameFactory.Create(_context, transitionType, _fovChanger, _timeProcess) as TransitionFov;
                    _context.ExplorationManager.SetTransition(_transitionFov);
                    break;
            }
        }
    }
}
