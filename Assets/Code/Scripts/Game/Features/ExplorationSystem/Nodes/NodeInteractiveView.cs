namespace ProjectM.Features.ExplorationSystem.Nodes
{
    using ProjectM.Constants;
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Generic.Managers.Main;

    public class NodeInteractiveView : NodeInteractive
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnExitButtonViewArt, BackToALinkedNode);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnExitButtonViewArt, BackToALinkedNode);
        }

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

        private void BackToALinkedNode()
        {
            int randomIndex = Random.Range(0, LinkedNodes.Count);
            LinkedNodes[randomIndex].Interact();
        }

#if UNITY_EDITOR
        protected override void OnDrawGizmos()
        {
            base.OnDrawGizmos();
            DrawPreview();
        }

        protected override void DrawLabel()
        {
            UnityEditor.Handles.Label(transform.position, "-View");
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