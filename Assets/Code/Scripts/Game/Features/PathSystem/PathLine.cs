namespace ProjectM.Features.PathSystem
{
    using UnityEngine;

    [RequireComponent(typeof(LineRenderer))]
    public class PathLine : MonoBehaviour
    {
        private LineRenderer _lineRenderer;
        private Vector3[] _lineNodes;

        private void Awake()
        {
            TryGetComponent(out _lineRenderer);
            _lineNodes = new Vector3[_lineRenderer.positionCount];
            _lineRenderer.GetPositions(_lineNodes);
        }

        public Vector3[] GetPathPositions()
        {
            return _lineNodes;
        }
    }
}
