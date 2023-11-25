﻿namespace ProjectM.Features.ExplorationSystem.Nodes
{
    using UnityEngine;
    using VUDK.Extensions;

    [DisallowMultipleComponent]
    public class NodeTarget : MonoBehaviour
    {
#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            DrawNode();
        }

        private void DrawNode()
        {
            Gizmos.color = Color.yellow;
            float size = transform.lossyScale.magnitude / 4f;
            GizmosExtension.DrawWireCubeWithRotation(transform.position, transform.rotation, Vector3.one * size);
            GizmosExtension.DrawArrowRay(transform.position, transform.forward * size);
        }
#endif
    }
}
