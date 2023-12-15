namespace VUDK.Features.Main.PointsSystem
{
    using UnityEngine;
    using VUDK.Features.Main.PointsSystem.Data;
    using VUDK.Features.Main.PointsSystem.Events;
    using VUDK.Features.Main.SaveSystem.Bases;
    using VUDK.Features.More.DateTaskSystem;
    using VUDK.Generic.Serializable;

    public class PointsManager : BinarySaverBase<PointsSaveData>
    {
        [Header("Points Settings")]
        [SerializeField, Min(0)]
        private int _initPoints;
        [SerializeField]
        private Range<int> _pointsLimits;

        public int Points { get; private set; }

        private void Start()
        {
            Load(nameof(PointsManager) + GetInstanceID(), ".save");
        }

        private void OnEnable()
        {
            PointsEvents.ModifyPointsHandler += ModifyPoints;
        }

        private void OnDisable()
        {
            PointsEvents.ModifyPointsHandler -= ModifyPoints;
        }

        public void Init(int startingPoints)
        {
            Points = startingPoints;
            PointsEvents.OnPointsInit?.Invoke(Points);
        }

        public void ModifyPoints(int pointsToModify)
        {
            int modifiedPoints = Mathf.Clamp(Points + pointsToModify, _pointsLimits.Min, _pointsLimits.Max) - Points;

            Points += modifiedPoints;
            PointsEvents.OnPointsChanged?.Invoke(this, modifiedPoints);

            SaveData.Points = Points;
            Save(nameof(PointsManager) + GetInstanceID(), ".save");
        }

        protected override void OnLoadSuccess(PointsSaveData saveData)
        {
            Init(saveData.Points);
        }

        protected override void OnLoadFail()
        {
            SaveData = new PointsSaveData(_initPoints); // if it fails it means there is no save data
            Init(_initPoints);
        }
    }
}