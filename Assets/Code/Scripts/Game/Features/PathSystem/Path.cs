namespace ProjectM.Features.PathSystem
{
    using UnityEngine;
    using VUDK.Extensions.Mathematics;

    public class Path : MonoBehaviour
    {
        [Header("Path")]
        [Tooltip("The number of points to calculate on the path")]
        [SerializeField, Range(1, 1000)]
        private int _resolution;
        [field: SerializeField]
        public Transform[] Waypoints { get; private set; }

        public Vector3[] Points { get; private set; }

        private void Awake()
        {
            CalculatePoints();
        }

        [ContextMenu("View Path")]
        private void CalculatePoints()
        {
            if(Waypoints.Length < 2) return;

            Points = MathExtension.GenerateCatmullRomSpline(Waypoints, _resolution);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.yellow;
            DrawLines();
            DrawWaypoint();
        }

        private void DrawLines()
        {
            if(Points == null) return;

            for (int i = 0; i < Points.Length - 1; i++)
                Gizmos.DrawLine(Points[i], Points[i + 1]);
        }

        private void DrawWaypoint()
        {
            foreach(Transform waypoint in Waypoints)
                Gizmos.DrawSphere(waypoint.position, 0.5f);
        }
#endif
    }
}
