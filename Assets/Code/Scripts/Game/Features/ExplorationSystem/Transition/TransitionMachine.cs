namespace ProjectM.Features.ExplorationSystem.Transition
{
    using UnityEngine;
    using VUDK.Generic.Serializable;
    using VUDK.Patterns.StateMachine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Features.Main.Camera.CameraModifiers;
    using ProjectM.Managers;
    using ProjectM.Patterns.Factories;
    using ProjectM.Features.ExplorationSystem.Transition.Phases;
    using ProjectM.Features.ExplorationSystem.Transition.Types.Keys;
    using ProjectM.Features.ExplorationSystem.Transition.Phases.Keys;

    public class TransitionMachine : StateMachine, ICastGameManager<GameManager>
    {
        [Header("Transition Settings")]
        [SerializeField]
        private TransitionType _transitionType;

        [SerializeField]
        private TimeDelayTask _timeProcess;
        [SerializeField]
        private CameraFovChanger _fovChanger;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        private ExplorationManager _explorationManager => GameManager.ExplorationManager;

        protected virtual void Awake()
        {
            SetTransition(_transitionType);
        }

        protected override void Start()
        {
            base.Start();
            Init();
        }

        public override void Init()
        {
            TransitionContext context = MachineFactory.Create();

            TransitionBegin beginPhase = MachineFactory.Create(TransitionStateKey.Start, this, context) as TransitionBegin;
            TransitionProcess processPhase = MachineFactory.Create(TransitionStateKey.Process, this, context) as TransitionProcess;
            TransitionEnd endPhase = MachineFactory.Create(TransitionStateKey.End, this, context) as TransitionEnd;

            AddState(TransitionStateKey.Start, beginPhase);
            AddState(TransitionStateKey.Process, processPhase);
            AddState(TransitionStateKey.End, endPhase);
        }

        public void SetTransition(TransitionType transitionType)
        {
            _explorationManager.SetTransition(GameFactory.Create(transitionType, _fovChanger, _timeProcess));
        }
    }
}
