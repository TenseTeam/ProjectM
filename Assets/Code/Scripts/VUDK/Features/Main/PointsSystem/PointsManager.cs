namespace VUDK.Features.Main.PointsSystem
{
    using UnityEngine;
    using VUDK.Features.Main.PointsSystem.Events;
    using VUDK.Generic.Serializable;

    public class PointsManager : MonoBehaviour
    {
        [Header("Points Settings")]
        [SerializeField]
        private bool _initOnAwake;
        [SerializeField]
        private int _initPoints;
        [SerializeField]
        private Range<int> _pointsLimits;

        public int Points { get; private set; }

        private void Awake()
        {
            if (_initOnAwake)
                Init(_initPoints);
        }

        private void OnEnable()
        {
            PointsEvents.ModifyPointsHandler += ModifyPoints;
        }

        private void OnDisable()
        {
            PointsEvents.ModifyPointsHandler -= ModifyPoints;
        }

        public void Init(int startingPoints) // TODO: Use this method then to manage different saving systems
        {
            Points = startingPoints;
            PointsEvents.OnPointsInit?.Invoke(Points);
        }

        public void ModifyPoints(int pointsToModify)
        {
            int modifiedPoints = Mathf.Clamp(Points + pointsToModify, _pointsLimits.Min, _pointsLimits.Max) - Points;

            Points += modifiedPoints;
            PointsEvents.OnPointsChanged?.Invoke(this, modifiedPoints);
        }
    }
}