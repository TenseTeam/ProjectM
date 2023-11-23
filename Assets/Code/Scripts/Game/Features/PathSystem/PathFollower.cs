namespace ProjectM.Features.PathSystem
{
    using UnityEngine;
    using Cinemachine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Extensions.Mathematics;
    using ProjectM.Constants;
    using ProjectM.Features.PathSystem.Data;

    [RequireComponent(typeof(CinemachineDollyCart))]
    public class PathFollower : MonoBehaviour
    {
        [Header("Follower Settings")]
        [SerializeField]
        private float _targetSpeed;

        [SerializeField]
        private Camera _camera;

        private int _targetWaypointIndex;
        private bool _isRunning;

        public CinemachineDollyCart Cart { get; private set; }

        public bool IsOnTargetWaypoint => Cart.m_Position.IsApproximatelyEqual(_targetWaypointIndex);

        private void Awake()
        {
            TryGetComponent(out CinemachineDollyCart cart);
            Cart = cart;
            Cart.m_PositionUnits = CinemachinePathBase.PositionUnits.PathUnits;
            Cart.m_Speed = 0f;
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

        private void Update()
        {
            OnRunningCart();
        }

        private void StartRun(int waypoint)
        {
            if (_isRunning) return;

            _isRunning = true;
            SetWaypointIndex(waypoint);
            Cart.m_Speed = GetCorrectSpeed();
        }

        private void SetWaypointIndex(int index)
        {
            _targetWaypointIndex = index;
        }

        private void ChangeTrack(PathTrackChangeEventData path)
        {
            if(path.NewPath == Cart.m_Path) return;

            Cart.m_Path = path.NewPath;
            Cart.m_Position = path.StartWaypointOnChange;
        }

        private float GetCorrectSpeed()
        {
            return _targetWaypointIndex > Cart.m_Position ? _targetSpeed : -_targetSpeed;
        }

        private void OnRunningCart()
        {
            if(!_isRunning) return;

            if (IsOnTargetWaypoint) // Reached target waypoint
            {
                Debug.Log("Reached target waypoint");
                Cart.m_Speed = 0f;
                _isRunning = false;
            }
        }
    }
}
