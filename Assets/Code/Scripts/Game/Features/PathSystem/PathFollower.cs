namespace ProjectM.Features.PathSystem
{
    using UnityEngine;
    using Cinemachine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Extensions.Mathematics;
    using ProjectM.Constants;
    using ProjectM.Features.PathSystem.Data;

    public class PathFollower : CinemachineDollyCart
    {
        [Header("Follower Settings")]
        [SerializeField]
        private float _targetSpeed;
        [SerializeField]
        private Transform _cameraTarget;

        private int _targetWaypointIndex;
        private bool _isRunning;

        public bool IsOnTargetWaypoint => m_Position.IsApproximatelyEqual(_targetWaypointIndex);

        private void Awake()
        {
            m_PositionUnits = CinemachinePathBase.PositionUnits.PathUnits;
            m_Speed = 0f;
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener<PathEventData>(GameEventKeys.OnNodeTrackSelected, StartRun);
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnCrossNodeSelected, ReturnToBegin);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<PathEventData>(GameEventKeys.OnNodeTrackSelected, StartRun);
            MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnCrossNodeSelected, ReturnToBegin);
        }

        protected override void Update()
        {
            base.Update();
            OnRunningCart();
        }

        private void StartRun(PathEventData pathEventData)
        {
            if (_isRunning) return;

            _isRunning = true;
            SetTrackAndWaypoint(pathEventData.PathToFollow, pathEventData.WaypointTargetIndex);
            m_Speed = GetCorrectSpeed();
        }

        private void ReturnToBegin()
        {
            SetWaypointIndex(0);
            m_Speed = GetCorrectSpeed();
        }

        private void SetTrackAndWaypoint(CinemachinePathBase path, int index)
        {
            SetPath(path);
            SetWaypointIndex(index);
        }

        private void SetWaypointIndex(int index)
        {
            _targetWaypointIndex = index;
        }

        private void SetPath(CinemachinePathBase path)
        {
            if(path == m_Path) return;

            Quaternion rot = _cameraTarget.rotation;
            m_Path = path;
            _cameraTarget.rotation = rot;
        }

        private float GetCorrectSpeed()
        {
            return _targetWaypointIndex > m_Position ? _targetSpeed : -_targetSpeed;
        }

        private void OnRunningCart()
        {
            if(!_isRunning) return;

            if (IsOnTargetWaypoint) // Reached target waypoint
            {
                Debug.Log("Reached target waypoint");
                m_Speed = 0f;
                _isRunning = false;
            }
        }
    }
}
