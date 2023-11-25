namespace ProjectM.Features.ExplorationSystem.Nodes
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Extensions.Gizmos;
    using ProjectM.Managers;
    using ProjectM.Constants;

    public class NodeInteractive : NodeBase, ICastGameManager<GameManager>
    {
        [SerializeField]
        private Button _interactButton;

        [Header("Linked Nodes")]
        [SerializeField]
        private NodeInteractive[] _linkedNodes;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        private PathExplorer _pathExplorer => GameManager.ExplorationManager.PathExplorer;

        private void OnValidate()
        {
            if (!_interactButton)
                _interactButton = GetComponentInChildren<Button>();
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnChangedNode, DisableInteraction);
            _interactButton.onClick.AddListener(InteractNode);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnChangedNode, DisableInteraction);
            _interactButton.onClick.RemoveListener(InteractNode);
        }

        private void Update()
        {
            LookAtExplorer();
        }

        public override void FirstNode()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnChangedNode);
            DisableInteraction();
            EnableLinkedNodesInteraction();
        }

        public void EnableInteraction()
        {
            _interactButton.gameObject.SetActive(true);
        }

        public void DisableInteraction()
        {
            _interactButton.gameObject.SetActive(false);
        }

        private void InteractNode()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnNodeInteract, this);
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnChangedNode);
            DisableInteraction();
            EnableLinkedNodesInteraction();
        }

        private void LookAtExplorer()
        {
            Vector3 direction = _pathExplorer.transform.position - _interactButton.transform.parent.position;
            _interactButton.transform.parent.rotation = Quaternion.LookRotation(-direction, Vector3.up);
        }

        private void EnableLinkedNodesInteraction()
        {
            foreach (var node in _linkedNodes)
                node.EnableInteraction();
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            DrawButtonLine();

            if(UnityEditor.Selection.activeGameObject == gameObject)
                DrawArrows();
            else
                DrawLinks();
        }

        private void DrawLinks()
        {
            if (_linkedNodes.Length == 0) return;

            Gizmos.color = Color.cyan;
            foreach (var node in _linkedNodes)
            {
                if (!node) continue;
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }

        private void DrawArrows()
        {
            if(_linkedNodes.Length == 0) return;

            Gizmos.color = Color.yellow;
            foreach (var node in _linkedNodes)
            {
                if(!node) continue;
                GizmosExtension.DrawArrow(transform.position, node.transform.position, Vector3.up * 160f, 2f);
            }
        }

        private void DrawButtonLine()
        {
            if(!_interactButton) return;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(_interactButton.transform.position, transform.position);
        }
#endif
    }
}
