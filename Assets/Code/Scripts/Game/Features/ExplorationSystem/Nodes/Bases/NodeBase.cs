namespace ProjectM.Features.ExplorationSystem.Nodes
{
    using UnityEngine;
    using UnityEngine.Events;
    using VUDK.Features.Main.InteractSystem.Interfaces;
    using VUDK.Generic.Managers.Main;
    using ProjectM.Constants;
    using ProjectM.Features.ExplorationSystem.Transition.Types.Keys;

    public abstract class NodeBase : MonoBehaviour, IInteractable
    {
        [field: Header("Node Settings")]
        [field: SerializeField]
        protected NodeTarget NodeTarget { get; private set; }

        [field: Header("Transition Settings")]
        [field: SerializeField]
        protected bool HasCustomTransition { get; private set; }
        [field: SerializeField]
        protected TransitionType CustomTransition { get; private set; }

        [Header("Node Events")]
        [SerializeField]
        private UnityEvent _onEnter;
        [SerializeField]
        private UnityEvent _onExit;

        protected ExplorationManager ExplorationManager { get; private set; }

        protected PathExplorer PathExplorer => ExplorationManager.PathExplorer;
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

        protected virtual void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<ExplorationManager>(GameEventKeys.OnExplorationManagerInit, Init);
        }

        protected virtual void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<ExplorationManager>(GameEventKeys.OnExplorationManagerInit, Init);
        }

        public virtual void Init(ExplorationManager explorationManager)
        {
            ExplorationManager = explorationManager;
        }

        [ContextMenu("Interact")]
        public virtual void Interact()
        {
            CheckCustomTransition();
            ChangeTargetNode();
            OnNodeChangedHandler();
            Disable();
        }

        public virtual void OnFirstNode()
        {
            PathExplorer.transform.SetPositionAndRotation(NodePosition, NodeRotation);
            OnNodeChangedHandler();
            Disable();
        }

        public abstract void Enable();

        public abstract void Disable();

        public virtual void OnNodeEnter()
        {
            _onEnter?.Invoke();
        }

        public virtual void OnNodeExit()
        {
            _onExit?.Invoke();
        }

        protected void OnNodeChangedHandler()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnChangedNode);
        }

        protected void ChangeTargetNode()
        {
            ExplorationManager.ChangeTargetNode(this);
        }

        protected void CheckCustomTransition()
        {
            if (HasCustomTransition)
                PathExplorer.ChangeTransitionType(CustomTransition);
            else
                PathExplorer.ResetToDefaultTransition();
        }

#if UNITY_EDITOR
        protected virtual void OnDrawGizmos()
        {
            Gizmos.color = Color.red;
            DrawLabel();
        }

        protected virtual void DrawLabel()
        {
            UnityEditor.Handles.Label(transform.position, $"-Node");
        }

        protected bool IsNodeSelectedInScene()
        {
            return
                UnityEditor.Selection.activeGameObject == gameObject ||
                UnityEditor.Selection.activeObject == NodeTarget.gameObject;
        }
#endif
    }
}