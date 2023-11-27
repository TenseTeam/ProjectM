namespace ProjectM.Features.ExplorationSystem.Nodes
{
    using ProjectM.Constants;
    using UnityEditor.Experimental.GraphView;
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Generic.Managers.Main;

    public class NodeInteractiveView : NodeInteractive
    {
        [SerializeField]
        private bool _useBackTargetNode;
        [SerializeField]
        private NodeBase _backTargetNode;

        public override void NodeEnter()
        {
            base.NodeEnter();
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnEnterNodeView);
        }

        public override void NodeExit()
        {
            base.NodeExit();
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnExitNodeView);
        }

        public void InteractNextNode()
        {
            if (_useBackTargetNode)
            {
                _backTargetNode.Interact();
                return;
            }

            int randomIndex = Random.Range(0, LinkedNodes.Count);
            LinkedNodes[randomIndex].Interact();
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            DrawArrowToBack();
            DrawPreview();
        }

        protected override void DrawLabel()
        {
            UnityEditor.Handles.Label(transform.position, "-View");

        }

        private void DrawArrowToBack()
        {
            if (!_backTargetNode) return;
            Gizmos.color = Color.yellow;
            GizmosExtension.DrawArrow(transform.position, _backTargetNode.transform.position, transform.lossyScale.magnitude);
        }

        private void DrawPreview()
        {
            if (IsNodeSelectedInScene())
            {
                if (!Camera.main) return;

                Camera cam = Camera.main;
                GizmosExtension.DrawCameraFrustum(NodeTarget.transform, cam.fieldOfView, cam.nearClipPlane, cam.farClipPlane, cam.aspect);
            }
        }
#endif
    }
}