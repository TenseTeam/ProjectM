namespace ProjectM.Features.PathSystem
{
    using Cinemachine;
    using ProjectM.Constants;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Serializable;

    public class PathCrossNode : PathNodeBase
    {
        [Header("Path Cross Node Settings")]
        [SerializeField]
        private SerializableDictionary<CinemachinePathBase, int> _correctPositionByPath;

        [SerializeField]
        private PathFollower _pathFollower;

        private void OnValidate()
        {
            if (_pathFollower == null) _pathFollower = FindObjectOfType<PathFollower>();
        }

        public override void TargetNode()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnCrossNodePathChosen, _correctPositionByPath[_pathFollower.m_Path]);
        }
    }
}
