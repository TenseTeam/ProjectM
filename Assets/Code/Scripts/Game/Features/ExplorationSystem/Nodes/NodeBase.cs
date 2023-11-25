namespace ProjectM.Features.ExplorationSystem.Nodes
{
    using UnityEngine;

    public abstract class NodeBase : MonoBehaviour
    {
        [Header("Node Settings")]
        [SerializeField]
        private Vector3 _offset;

        public Vector3 NodePosition => transform.position + _offset;

        public abstract void FirstNode();

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            float sizeRadius = transform.lossyScale.magnitude / 2f;
            Gizmos.DrawWireSphere(transform.position, sizeRadius / 2f);
            Gizmos.DrawSphere(NodePosition, sizeRadius);
            Gizmos.DrawLine(transform.position, NodePosition);
        }
#endif
    }
}
