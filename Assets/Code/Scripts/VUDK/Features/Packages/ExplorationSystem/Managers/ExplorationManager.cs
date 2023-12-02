namespace VUDK.Features.Packages.ExplorationSystem.Managers
{
    using UnityEngine;
    using VUDK.Features.Packages.ExplorationSystem.Constants;
    using VUDK.Features.Packages.ExplorationSystem.Nodes;
    using VUDK.Features.Packages.ExplorationSystem.Transition.Phases.Keys;
    using VUDK.Features.Packages.ExplorationSystem.Transition.Types;
    using VUDK.Features.Packages.ExplorationSystem.Explorers;
    using VUDK.Patterns.Initialization;
    using VUDK.Features.Main.EventSystem;

    [DefaultExecutionOrder(-100)]
    public class ExplorationManager : Initializer
    {
        [Header("Exploration Settings")]
        [SerializeField]
        private NodeBase _firstNode;

        [field: SerializeField]
        public PathExplorer PathExplorer { get; private set; }

        public NodeBase CurrentTargetNode { get; private set; }
        public NodeBase PreviousTargetNode { get; private set; }
        public TransitionBase CurrentTransition { get; private set; }

        protected virtual void OnValidate()
        {
            if (PathExplorer == null) PathExplorer = FindAnyObjectByType<PathExplorer>();
        }

        protected virtual void Awake()
        {
            if (_firstNode == null) Debug.LogError($"First Node in {nameof(ExplorationManager)} NOT Selected!");
        }

        protected virtual void OnEnable()
        {
            EventManager.Ins.AddListener(ExplorationEventKeys.OnExitObservationButton, ChangeTargetToPrevious);
        }

        protected virtual void OnDisable()
        {
            EventManager.Ins.RemoveListener(ExplorationEventKeys.OnExitObservationButton, ChangeTargetToPrevious);
        }

        protected virtual void Start()
        {
            Init();
        }

        public override void Init()
        {
            EventManager.Ins.TriggerEvent(ExplorationEventKeys.OnExplorationManagerInit, this);
            InitFirstNode();
            InitPathExplorer();
        }

        public override bool Check()
        {
            return PathExplorer && _firstNode;
        }

        public void SetTransition(TransitionBase transition)
        {
            if(transition == null) return;

            CurrentTransition = transition;
        }

        public void ChangeTargetToPrevious()
        {
            if (PreviousTargetNode == null) return;
            ChangeTargetNode(PreviousTargetNode);
        }

        public void ChangeTargetNode(NodeBase targetNode)
        {
            if (PathExplorer.IsState(TransitionStateKey.Process)) return;
            PreviousTargetNode = CurrentTargetNode;
            CurrentTargetNode = targetNode;
            PathExplorer.TransitionStart();
        }

        private void InitPathExplorer()
        {
            PathExplorer.Inject(this); // Initializing this here is more modular
            PathExplorer.Init();
            _firstNode.OnFirstNode();
        }

        private void InitFirstNode()
        {
            CurrentTargetNode = _firstNode;
            PreviousTargetNode = CurrentTargetNode;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(_firstNode == null) return;

            UnityEditor.Handles.Label(_firstNode.NodePosition, "-Start");
        }
#endif
    }
}
