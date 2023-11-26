namespace ProjectM.Features.ExplorationSystem.Nodes
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using VUDK.Extensions;
    using ProjectM.Managers;
    using ProjectM.Constants;
    using ProjectM.Features.ExplorationSystem.Transition.Types.Keys;

    public class NodeInteractive : NodeBase, ICastGameManager<GameManager>
    {
        [Header("Node Interaction Settings")]
        [SerializeField]
        protected Button InteractButton;

        [Header("Transition Settings")]
        [SerializeField]
        private bool _hasCustomTransition;
        [SerializeField]
        private TransitionType _customTransition;

        [Header("Linked Nodes")]
        [SerializeField]
        protected List<NodeInteractive> LinkedNodes;

        private void OnValidate()
        {
            if (!InteractButton)
            {
                InteractButton = GetComponentInChildren<Button>();
                if (!InteractButton)
                    Debug.LogError("No button found in children", gameObject);
            }
        }

        protected virtual void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnChangedNode, DisableInteraction);
            InteractButton.onClick.AddListener(Interact);
        }

        protected virtual void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnChangedNode, DisableInteraction);
            InteractButton.onClick.RemoveListener(Interact);
        }

        protected virtual void Update()
        {
            LookAtExplorer();
        }

        public override void StartFirstNode()
        {
            base.StartFirstNode();
            EnableLinkedNodesInteraction();
        }

        public override void EnableInteraction()
        {
            InteractButton.gameObject.SetActive(true);
        }

        public override void DisableInteraction()
        {
            InteractButton.gameObject.SetActive(false);
        }

        public override void Interact()
        {
            if (_hasCustomTransition)
                GameManager.ExplorationManager.PathExplorer.ChangeTransitionType(_customTransition);
            else
                GameManager.ExplorationManager.PathExplorer.DefaultTransition();

            base.Interact();
        }

        public override void NodeEnter()
        {
            base.NodeEnter();
            EnableLinkedNodesInteraction();
        }

        private void LookAtExplorer()
        {
            Vector3 direction = PathExplorer.transform.position - InteractButton.transform.parent.position;
            InteractButton.transform.parent.rotation = Quaternion.LookRotation(-direction, Vector3.up);
        }

        private void EnableLinkedNodesInteraction()
        {
            foreach (var node in LinkedNodes)
                node.EnableInteraction();
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            DrawLinkWithTargetNode();
            DrawButtonLine();

            if (IsNodeSelectedInScene())
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
            if (LinkedNodes.Count == 0) return;

            Gizmos.color = Color.cyan;
            foreach (var node in LinkedNodes)
            {
                if (!node) continue;
                Gizmos.DrawLine(transform.position, node.transform.position);
            }
        }

        private void DrawArrows()
        {
            if (LinkedNodes.Count == 0) return;

            Gizmos.color = Color.yellow;
            foreach (var node in LinkedNodes)
            {
                if(!node) continue;
                GizmosExtension.DrawArrow(transform.position, node.transform.position, transform.lossyScale.magnitude);
            }
        }

        private void DrawButtonLine()
        {
            if(!InteractButton) return;

            Gizmos.color = Color.red;
            Gizmos.DrawLine(InteractButton.transform.position, transform.position);
        }
#endif
    }
}
