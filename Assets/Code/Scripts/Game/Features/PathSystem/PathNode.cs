namespace ProjectM.Features.PathSystem
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectM.Constants;

    public class PathNode : PathNodeBase
    {
        [Header("Path Node Settings")]
        [SerializeField]
        private int _waypointIndex;

        //[Header("Next Nodes Settings")]
        //[SerializeField]
        //private PathNode[] _nextNodes;
        
        public override void TargetNode()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnNodeTrackSelected, _waypointIndex);
            //SelectNode();
        }

        //public void SelectNode()
        //{
        //    MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnNodeSelected);
        //}

        //public void SelectNodeAsStart()
        //{
        //    SelectNode();
        //    EnableNextNodes();
        //}

        //private void EnableNextNodes()
        //{
        //    foreach (PathNode node in _nextNodes)
        //        node.gameObject.SetActive(true);
        //}

        //private void DisableNode()
        //{
        //    gameObject.SetActive(false);
        //}
    }
}
