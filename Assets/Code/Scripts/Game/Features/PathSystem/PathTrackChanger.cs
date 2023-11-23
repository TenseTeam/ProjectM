namespace ProjectM.Features.PathSystem
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectM.Constants;
    using ProjectM.Features.PathSystem.Data;

    public class PathTrackChanger : PathNode
    {
        [SerializeField]
        private PathTrackChangeEventData _pathTrackChangeEventData;

        public override void TargetNode()
        {
            base.TargetNode();
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnTryTrackChange, _pathTrackChangeEventData);
        }
    }
}
