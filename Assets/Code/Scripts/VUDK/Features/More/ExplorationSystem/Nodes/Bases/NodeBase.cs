namespace VUDK.Features.More.ExplorationSystem.Nodes
{
    using UnityEngine;
    using UnityEngine.Events;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Main.InteractSystem.Interfaces;
    using VUDK.Features.More.ExplorationSystem.Constants;
    using VUDK.Features.More.ExplorationSystem.Explorers;
    using VUDK.Features.More.ExplorationSystem.Managers;
    using VUDK.Features.More.ExplorationSystem.Nodes.Components;
    using VUDK.Features.More.ExplorationSystem.Transition.Types.Keys;

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

        protected virtual void Awake()
        {
            Disable();
        }

        protected virtual void OnEnable()
        {
            EventManager.Ins.AddListener<ExplorationManager>(ExplorationEventKeys.OnExplorationManagerInit, Init);
        }

        protected virtual void OnDisable()
        {
            EventManager.Ins.RemoveListener<ExplorationManager>(ExplorationEventKeys.OnExplorationManagerInit, Init);
        }

        public virtual void Init(ExplorationManager explorationManager)
        {
            ExplorationManager = explorationManager;
        }

        [ContextMenu("Interact")]
        public virtual void Interact()
        {
            CheckCustomTransition();
            GoToThisNode();
            OnNodeChangedHandler();
            Disable();
        }

        public virtual void OnFirstNode(TransitionType transitionType)
        {
            PathExplorer.ChangeTransitionType(transitionType);
            GoToThisNode();
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
            EventManager.Ins.TriggerEvent(ExplorationEventKeys.OnChangedNode);
        }

        protected void GoToThisNode()
        {
            ExplorationManager.GoToNode(this);
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