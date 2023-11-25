namespace ProjectM.Features.ExplorationSystem.Nodes
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectM.Constants;

    public class NodeInteractiveView : NodeInteractive
    {
        protected override void OnEnable()
        {
            base.OnEnable();
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnExitButtonViewArt, BackToALinkedNode);
        }

        protected override void OnDisable()
        {
            base.OnDisable();
            MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnExitButtonViewArt, BackToALinkedNode);
        }

        public override void NodeEnter()
        {
            base.NodeEnter();
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnEnterNodeView);
        }

        public override void NodeExit()
        {
            base.NodeExit();
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnExitNodeView);
        }

        private void BackToALinkedNode()
        {
            int randomIndex = Random.Range(0, LinkedNodes.Count);
            LinkedNodes[randomIndex].Interact();
        }
    }
}
