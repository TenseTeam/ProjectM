namespace VUDK.Features.Main.PointsSystem.Reward
{
    using UnityEngine;
    using VUDK.Features.Main.PointsSystem.Events;

    public class RewardTrigger : MonoBehaviour
    {
        [Header("Reward Settings")]
        [SerializeField]
        private bool _isOnce = true;
        [SerializeField]
        private int _rewardPoints;

        private bool _isObtained;

        public void TriggerReward()
        {
            if (_isOnce && _isObtained) return;
            _isObtained = true;
            PointsEvents.ModifyPointsHandler?.Invoke(_rewardPoints);
        }
    }
}