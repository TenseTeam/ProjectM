namespace ProjectM.Managers
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectM.Features.UI.UIExploration;
    using VUDK.Generic.Managers.Main;
    using ProjectM.Constants;

    public class GameUIManager : UIManagerBase
    {
        [field: SerializeField]
        public ObservationPanel ObservationPanel { get; private set; }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnEnterNodeView, ObservationPanel.Enable);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnEnterNodeView, ObservationPanel.Enable);
        }
    }
}
