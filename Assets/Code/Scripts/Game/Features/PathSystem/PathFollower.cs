namespace ProjectM.Features.PathSystem
{
    using UnityEngine;
    using Cinemachine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Extensions.Mathematics;
    using ProjectM.Constants;
    using ProjectM.Features.PathSystem.Data;
    using System;

    public class PathFollower : CinemachineDollyCart
    {
        [Header("Follower Settings")]
        [SerializeField]
        private float _targetSpeed;

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
            MainManager.Ins.EventManager.AddListener<int>(GameEventKeys.OnNodeTrackSelected, StartRun);
            MainManager.Ins.EventManager.AddListener<int>(GameEventKeys.OnCrossNodePathChosen, StartRun);
            MainManager.Ins.EventManager.AddListener<PathTrackChangeEventData>(GameEventKeys.OnTryTrackChange, ChangeTrack);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener<int>(GameEventKeys.OnNodeTrackSelected, StartRun);
            MainManager.Ins.EventManager.RemoveListener<int>(GameEventKeys.OnCrossNodePathChosen, StartRun);
            MainManager.Ins.EventManager.RemoveListener<PathTrackChangeEventData>(GameEventKeys.OnTryTrackChange, ChangeTrack);
        }

        protected override void Update()
        {
            base.Update();
            OnRunningCart();
        }

        private void StartRun(int waypoint)
        {
            if (_isRunning) return;

            _isRunning = true;
            SetWaypointIndex(waypoint);
            m_Speed = GetCorrectSpeed();
        }

        private void SetWaypointIndex(int index)
        {
            _targetWaypointIndex = index;
        }

        private void ChangeTrack(PathTrackChangeEventData path)
        {
            if(path.NewPath == m_Path) return;

            m_Path = path.NewPath;
            m_Position = path.StartWaypointOnChange;
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
