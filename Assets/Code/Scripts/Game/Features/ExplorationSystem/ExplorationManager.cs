namespace ProjectM.Features.ExplorationSystem
{
    using ProjectM.Constants;
    using ProjectM.Features.ExplorationSystem.Nodes;
    using ProjectM.Features.ExplorationSystem.Transition.Phases.Keys;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;

    public class ExplorationManager : MonoBehaviour
    {
        [field: Header("Exploration Settings")]
        [SerializeField]
        private NodeBase _firstNode;
        [field: SerializeField]
        public PathExplorer PathExplorer { get; private set; }

        public NodeBase CurrentTargetNode { get; private set; }

        private void OnValidate()
        {
            if (PathExplorer == null) PathExplorer = FindObjectOfType<PathExplorer>();
        }

        private void Start()
        {
            Init();
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<NodeBase>(GameEventKeys.OnNodeInteract, TargetNode);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<NodeBase>(GameEventKeys.OnNodeInteract, TargetNode);
        }

        public void Init()
        {
            PlaceExplorerAtStart();
            _firstNode.FirstNode();
        }

        private void TargetNode(NodeBase targetNode)
        {
            if (PathExplorer.IsState(TransitionStateKey.Process)) return;

            CurrentTargetNode = targetNode;
            PathExplorer.StartMachine();
        }

        private void PlaceExplorerAtStart()
        {
            PathExplorer.transform.position = _firstNode.NodePosition;
        }
    }
}
