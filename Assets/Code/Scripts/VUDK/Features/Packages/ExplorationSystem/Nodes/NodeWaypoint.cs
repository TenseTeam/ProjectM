namespace VUDK.Features.Packages.ExplorationSystem.Nodes
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Extensions;

    public class NodeWaypoint : NodeInteractiveBase
    {
        [Header("Links")]
        [SerializeField]
        protected List<NodeInteractiveBase> LinkedNodes;

        protected override void OnValidate()
        {
            base.OnValidate();
            LinkedNodes.RemoveAll(node => !node);
        }

        public override void OnFirstNode()
        {
            base.OnFirstNode();
            EnableLinkedNodes();
        }

        public override void OnNodeEnter()
        {
            base.OnNodeEnter();
            EnableLinkedNodes();
        }

        private void EnableLinkedNodes()
        {
            foreach (var node in LinkedNodes)
                node.Enable();
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            DrawArrows();
        }

        private void DrawArrows()
        {
            if (LinkedNodes.Count == 0) return;

            Gizmos.color = Color.yellow;
            foreach (var node in LinkedNodes)
            {
                if (!node) continue;
                GizmosExtension.DrawArrow(transform.position, node.transform.position, transform.lossyScale.magnitude);
            }
        }
#endif
    }
}
