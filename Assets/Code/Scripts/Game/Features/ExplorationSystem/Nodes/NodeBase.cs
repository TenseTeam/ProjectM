namespace ProjectM.Features.ExplorationSystem.Nodes
{
    using ProjectM.Managers;
    using UnityEngine;
    using UnityEngine.Events;
    using VUDK.Features.Main.InteractSystem.Interfaces;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;

    public abstract class NodeBase : MonoBehaviour, ICastGameManager<GameManager>, IInteractable
    {
        [field: Header("Node Settings")]
        [field: SerializeField]
        protected NodeTarget NodeTarget { get; private set; }

        [Header("Node Events")]
        public UnityEvent OnNodeEnter;
        public UnityEvent OnNodeExit;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        protected PathExplorer PathExplorer => GameManager.ExplorationManager.PathExplorer;

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

        public abstract void Interact();

        public abstract void EnableInteraction();

        public abstract void DisableInteraction();

        public virtual void StartFirstNode()
        {
            PathExplorer.transform.position = NodePosition;
            PathExplorer.transform.rotation = NodeRotation;
            DisableInteraction();
        }

        public virtual void NodeEnter()
        {
            OnNodeEnter?.Invoke();
        }

        public virtual void NodeExit()
        {
            OnNodeExit?.Invoke();
        }

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