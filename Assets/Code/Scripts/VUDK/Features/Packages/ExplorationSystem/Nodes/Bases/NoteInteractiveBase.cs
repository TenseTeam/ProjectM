﻿namespace VUDK.Features.Packages.ExplorationSystem.Nodes
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Features.Packages.ExplorationSystem.Constants;
    using VUDK.Features.Main.EventSystem;

    public abstract class NodeInteractiveBase : NodeBase
    {
        [Header("Node Interaction Settings")]
        [SerializeField]
        protected Button InteractButton;

        protected bool IsInteractable { get; private set; } = true;

        protected virtual void OnValidate()
        {
            if (!InteractButton)
            {
                InteractButton = GetComponentInChildren<Button>();
                if (!InteractButton)
                    Debug.LogError("No button found in children", gameObject);
            }
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            EventManager.Ins.AddListener(ExplorationEventKeys.OnChangedNode, Disable);
            InteractButton.onClick.AddListener(Interact);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            EventManager.Ins.RemoveListener(ExplorationEventKeys.OnChangedNode, Disable);
            InteractButton.onClick.RemoveListener(Interact);
        }

        protected virtual void Update() => LookAtExplorer();

        public override void Enable()
        {
            IsInteractable = true;
            InteractButton.gameObject.SetActive(true);
        }

        public override void Disable()
        {
            IsInteractable = false;
            InteractButton.gameObject.SetActive(false);
        }

        public override void Interact()
        {
            if (!IsInteractable) return;
            base.Interact();
        }

        private void LookAtExplorer()
        {
            Vector3 direction = PathExplorer.transform.position - InteractButton.transform.parent.position;
            InteractButton.transform.parent.rotation = Quaternion.LookRotation(-direction, Vector3.up);
        }
#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            DrawTargetNodeLine();
            DrawButtonLine();
        }

        private void DrawTargetNodeLine()
        {
            if (!NodeTarget) return;

            Gizmos.color = Color.yellow;
            Gizmos.DrawLine(transform.position, NodePosition);
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
