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

        private ExplorationManager _explorationManager;

        private TransitionType _defaultTransition;
        private TransitionType _currentTransitionType;

        #region Transitions Instances
        private TransitionContext _context;
        private TransitionInstant _transitionInstant;
        private TransitionLinear _transitionLinear;
        private TransitionFov _transitionFov;
        #endregion

        public virtual void Init(ExplorationManager explorationManager)
        {
            _explorationManager = explorationManager;
            _defaultTransition = _transitionType;
            Init();
            SetTransition(_defaultTransition);
        }

        public override void Init()
        {
            if(Check() == false) return;
            _context = MachineFactory.Create(_explorationManager);

            TransitionBegin beginPhase = MachineFactory.Create(TransitionStateKey.Start, this, _context) as TransitionBegin;
            TransitionProcess processPhase = MachineFactory.Create(TransitionStateKey.Process, this, _context) as TransitionProcess;
            TransitionEnd endPhase = MachineFactory.Create(TransitionStateKey.End, this, _context) as TransitionEnd;

            AddState(TransitionStateKey.Start, beginPhase);
            AddState(TransitionStateKey.Process, processPhase);
            AddState(TransitionStateKey.End, endPhase);
        }

        public override bool Check()
        {
            return _explorationManager != null;
        }

        public void ResetToDefaultTransition()
        {
            ChangeTransitionType(_defaultTransition);
        }

        public void ChangeTransitionType(TransitionType transitionType)
        {
            if(_currentTransitionType == transitionType) return;
            SetTransition(transitionType);
        }

        private void SetTransition(TransitionType transitionType)
        {
            _currentTransitionType = transitionType;
            TransitionBase transition = transitionType switch
            {
                TransitionType.Instant => _transitionInstant ??= GameFactory.Create(_context),
                TransitionType.Linear => _transitionLinear ??= GameFactory.Create(_context, _timeProcess),
                TransitionType.Fov => _transitionFov ??= GameFactory.Create(_context, _fovChanger, _timeProcess),
                _ => null,
            };

            _explorationManager.SetTransition(transition);
        }
    }
}
