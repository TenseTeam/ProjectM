namespace ProjectM.Features.PathSystem
{
    using ProjectM.Constants;
    using System.IO;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;

    public class PathRunner : MonoBehaviour
    {
        [SerializeField, Header("Movement")]
        private float _speed = 1f;
        [SerializeField]
        private Vector3 _offset;

        private bool _isRunningPath = false;
        private int _currentNodeIndex = 0;

        private Vector3[] _positions;

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<Vector3[]>(GameEventKeys.OnPathTriggered, StartPath);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<Vector3[]>(GameEventKeys.OnPathTriggered, StartPath);
        }

        private void Update() => RunPath();

        /// <summary>
        /// Starts the character pathing.
        /// </summary>
        public void StartPath(Vector3[] positions)
        {
            if (_isRunningPath) return;

            _positions = positions;
            _isRunningPath = true;
        }

        /// <summary>
        /// Runs the character path.
        /// </summary>
        private void RunPath()
        {
            if (!_isRunningPath) return;

            if (_currentNodeIndex >= _positions.Length)
            {
                ReachDestination();
                return;
            }

            Vector3 _pos = _positions[_currentNodeIndex] + _offset;

            float distance = Vector3.Distance(transform.position, _pos);
            float t = Time.deltaTime * _speed / distance;
            transform.position = Vector3.Lerp(transform.position, _pos, t);

            if (distance < 0.01f)
            {
                transform.position = _pos;
                _currentNodeIndex++;
            }
        }

        /// <summary>
        /// Called when the character reaches its destination.
        /// </summary>
        private void ReachDestination()
        {
            _isRunningPath = false;
            _currentNodeIndex = 0;
        }
    }
}