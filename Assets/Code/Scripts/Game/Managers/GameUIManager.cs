namespace ProjectM.Managers
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectM.Features.UI.UIExploration;

    public class GameUIManager : UIManagerBase
    {
        [field: SerializeField, Header("Observation Panel")]
        public ObservationPanel ObservationPanel { get; private set; }
    }
}
