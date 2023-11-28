namespace VUDK.Features.Packages.ExplorationSystem.Nodes
{
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Packages.ExplorationSystem.Constants;

    public class NodeObservation : NodeInteractiveBase
    {
        public override void OnNodeEnter()
        {
            base.OnNodeEnter();
            EventManager.Ins.TriggerEvent(ExplorationEventKeys.OnEnterNodeObservation);
        }

        public override void OnNodeExit()
        {
            base.OnNodeExit();
            EventManager.Ins.TriggerEvent(ExplorationEventKeys.OnExitNodeObservation);
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