namespace ProjectM.Features.ExplorationSystem.Nodes
{
    using UnityEngine;

    public abstract class NodeBase : MonoBehaviour
    {
        [field: Header("Node Settings")]
        [field: SerializeField]
        protected NodeTarget NodeTarget { get; private set; }

        public Quaternion NodeRotation => NodeTarget.transform.rotation;
        public Vector3 NodePosition => NodeTarget.transform.position;

        private void OnValidate()
        {
            if (!NodeTarget)
            {
                NodeTarget = GetComponentInChildren<NodeTarget>();
                if (!NodeTarget)
                    Debug.LogError($"NodeTarget not found in {gameObject.name}");
            }
        }

        public abstract void FirstNode();

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            UnityEditor.Handles.Label(transform.position, $"---Node");
            Gizmos.DrawSphere(transform.position, transform.localScale.magnitude / 6f);
        }
#endif
    }
}