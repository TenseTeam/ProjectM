namespace VUDK.Extensions
{
    using UnityEngine;

    public static class GizmosExtension
    {
        public static void DrawArrowRay(Vector3 pos, Vector3 direction, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Gizmos.DrawRay(pos, direction);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * Vector3.forward;
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * Vector3.forward;
            Gizmos.DrawRay(pos + direction, right * arrowHeadLength);
            Gizmos.DrawRay(pos + direction, left * arrowHeadLength);
        }

        public static void DrawArrow(Vector3 from, Vector3 to, float arrowHeadLength = 0.25f, float arrowHeadAngle = 20.0f)
        {
            Gizmos.DrawLine(from, to);

            Vector3 direction = to - from;
            Vector3 arrowEnd = to - direction.normalized * arrowHeadLength;

            //Gizmos.DrawRay(to, (arrowEnd - to).normalized * arrowHeadLength);

            Vector3 right = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 + arrowHeadAngle, 0) * Vector3.forward;
            Vector3 left = Quaternion.LookRotation(direction) * Quaternion.Euler(0, 180 - arrowHeadAngle, 0) * Vector3.forward;
            Gizmos.DrawRay(arrowEnd, right * arrowHeadLength);
            Gizmos.DrawRay(arrowEnd, left * arrowHeadLength);
        }

        public static void DrawWireCubeWithRotation(Vector3 position, Quaternion rotation, Vector3 size)
        {
            Matrix4x4 originalMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(position, rotation, Vector3.one);
            Gizmos.DrawWireCube(Vector3.zero, size);
            Gizmos.matrix = originalMatrix;
        }
    }
}