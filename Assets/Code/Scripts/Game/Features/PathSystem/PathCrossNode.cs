namespace ProjectM.Features.PathSystem
{
    using ProjectM.Constants;
    using ProjectM.Features.PathSystem.Data;
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;

    public class PathCrossNode : PathNodeBase
    {
        public override void TargetNode()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnCrossNodeSelected);
        }
    }
}
