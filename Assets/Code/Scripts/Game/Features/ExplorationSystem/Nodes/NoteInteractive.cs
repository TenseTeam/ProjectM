namespace ProjectM.Features.ExplorationSystem.Nodes
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Features.Main.InteractSystem.Interfaces;
    using VUDK.Extensions;
    using ProjectM.Managers;
    using ProjectM.Constants;

    public class NodeInteractive : NodeBase, ICastGameManager<GameManager>, IInteractable
    {
        [SerializeField]
        private Button _interactButton;

        [Header("Linked Nodes")]
        [SerializeField]
        private List<NodeInteractive> _linkedNodes;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        private PathExplorer _pathExplorer => GameManager.ExplorationManager.PathExplorer;

        private void OnValidate()
        {
            if (!_interactButton)
            {
                _interactButton = GetComponentInChildren<Button>();
                if (!_interactButton)
                    Debug.LogError("No button found in children", gameObject);
            }
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnChangedNode, DisableInteraction);
            _interactButton.onClick.AddListener(Interact);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnChangedNode, DisableInteraction);
            _interactButton.onClick.RemoveListener(Interact);
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

        public void Interact()
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
            DrawLinkWithTargetNode();
            DrawButtonLine();

            if(UnityEditor.Selection.activeGameObject == gameObject)
                DrawArrows();
            else
                DrawLinks();
        }

        private void DrawLinkWithTargetNode()
        {
            if (!NodeTarget) return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, NodePosition);
        }

        private void DrawLinks()
        {
            if (_linkedNodes.Count == 0) return;

            Gizmos.color = Color.cyan;
            foreach (var node in _linkedNodes)
            {
                if (!node) continue;
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }

        private void DrawArrows()
        {
            if (_linkedNodes.Count == 0) return;

            Gizmos.color = Color.yellow;
            foreach (var node in _linkedNodes)
            {
                if(!node) continue;
                GizmosExtension.DrawArrow(transform.position, node.transform.position, transform.lossyScale.magnitude);
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
