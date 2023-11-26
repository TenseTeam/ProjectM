namespace ProjectM.Features.ExplorationSystem
{
    using UnityEngine;
    using ProjectM.Constants;
    using ProjectM.Features.ExplorationSystem.Nodes;
    using ProjectM.Features.ExplorationSystem.Transition.Phases.Keys;
    using ProjectM.Features.ExplorationSystem.Transition.Types;
    using VUDK.Generic.Managers.Main;
    using UnityEngine.Pool;

    public class ExplorationManager : MonoBehaviour
    {
        [field: Header("Exploration Settings")]
        [SerializeField]
        private NodeBase _firstNode;
        [field: SerializeField]
        public PathExplorer PathExplorer { get; private set; }

        public NodeBase CurrentTargetNode { get; private set; }
        public TransitionBase CurrentTransition { get; private set; }

        private void OnValidate()
        {
            if (PathExplorer == null) PathExplorer = FindObjectOfType<PathExplorer>();
        }

        private void Start()
        {
            _firstNode.StartFirstNode();
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<NodeBase>(GameEventKeys.OnNodeInteract, TargetNode);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<NodeBase>(GameEventKeys.OnNodeInteract, TargetNode);
        }

        public void SetTransition(TransitionBase transition)
        {
            CurrentTransition = transition;
        }

        private void TargetNode(NodeBase targetNode)
        {
            if (PathExplorer.IsState(TransitionStateKey.Process)) return;

            CurrentTargetNode = targetNode;
            PathExplorer.StartMachine();
        }
    }
}
