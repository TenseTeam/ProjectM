namespace VUDK.Features.Packages.ExplorationSystem.Transition
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.Main.Camera.CameraModifiers;
    using VUDK.Features.Packages.ExplorationSystem.Transition.Phases;
    using VUDK.Features.Packages.ExplorationSystem.Transition.Types.Keys;
    using VUDK.Features.Packages.ExplorationSystem.Transition.Phases.Keys;
    using VUDK.Features.Packages.ExplorationSystem.Transition.Types;
    using VUDK.Features.Packages.ExplorationSystem.Factory;
    using VUDK.Features.Packages.ExplorationSystem.Managers;
    using VUDK.Patterns.DependencyInjection.Interfaces;

    public class TransitionMachine : StateMachine, IInject<ExplorationManager>
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

        public virtual void Inject(ExplorationManager explorationManager)
        {
            _explorationManager = explorationManager;
        }

        public override void Init()
        {
            if(Check() == false) return;
            _defaultTransition = _transitionType;

            _context = ExplorationFactory.Create(_explorationManager);

            TransitionBegin beginPhase = ExplorationFactory.Create(TransitionStateKey.Start, this, _context) as TransitionBegin;
            TransitionProcess processPhase = ExplorationFactory.Create(TransitionStateKey.Process, this, _context) as TransitionProcess;
            TransitionEnd endPhase = ExplorationFactory.Create(TransitionStateKey.End, this, _context) as TransitionEnd;

            AddState(TransitionStateKey.Start, beginPhase);
            AddState(TransitionStateKey.Process, processPhase);
            AddState(TransitionStateKey.End, endPhase);
            SetTransition(_defaultTransition);
        }

        public override bool Check()
        {
            return _explorationManager != null;
        }

        public void ResetToDefaultTransition()
        {
            ChangeTransitionType(_defaultTransition);
        }

        public void ChangeDefaultTransition(TransitionType transitionType)
        {
            _defaultTransition = transitionType;
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
                TransitionType.Instant => _transitionInstant ??= ExplorationFactory.Create(_context),
                TransitionType.Linear => _transitionLinear ??= ExplorationFactory.Create(_context, _timeProcess),
                TransitionType.Fov => _transitionFov ??= ExplorationFactory.Create(_context, _fovChanger, _timeProcess),
                _ => null,
            };

            _explorationManager.SetTransition(transition);
        }
    }
}
