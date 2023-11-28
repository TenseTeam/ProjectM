namespace ProjectM.Features.ExplorationSystem
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.FactoryMethod;
    using ProjectM.Constants;
    using ProjectM.Features.ExplorationSystem.Nodes;
    using ProjectM.Features.ExplorationSystem.Transition.Phases.Keys;
    using ProjectM.Features.ExplorationSystem.Transition.Types;

    public class ExplorationManager : FactoryMethodBase
    {
        [Header("Exploration Settings")]
        [SerializeField]
        private NodeBase _firstNode;

        [field: SerializeField]
        public PathExplorer PathExplorer { get; private set; }

        public NodeBase CurrentTargetNode;
        public NodeBase PreviousTargetNode;
        public TransitionBase CurrentTransition { get; private set; }

        private void OnValidate()
        {
            if (PathExplorer == null) PathExplorer = FindObjectOfType<PathExplorer>();
        }

        private void Awake()
        {
            if (_firstNode == null) Debug.LogError($"First Node in {nameof(ExplorationManager)} NOT Selected!");
        }

        private void Start()
        {
            Init();
        }

        public override void Init()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnExplorationManagerInit, this);
            PathExplorer.Init(this);
            _firstNode.OnFirstNode();
            CurrentTargetNode = _firstNode;
            PreviousTargetNode = CurrentTargetNode;
        }

        public override bool Check()
        {
            return PathExplorer && _firstNode;
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnExitObservationButton, ChangeTargetToPrevious);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnExitObservationButton, ChangeTargetToPrevious);
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

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            if(_firstNode == null) return;

            UnityEditor.Handles.Label(_firstNode.NodePosition, "--Start Node");
        }
#endif
    }
}
