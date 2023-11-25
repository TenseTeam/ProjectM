namespace ProjectM.Managers
{
    using UnityEngine;
    using VUDK.Features.Main.InputSystem.MobileInputs;
    using VUDK.Features.More.WeatherSystem;
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectM.Features.ExplorationSystem;

    public class GameManager : GameManagerBase
    {
        [field: SerializeField, Header("Mobile Inputs Manager")]
        public MobileInputsManager MobileInputsManager { get; private set; }

        [field: SerializeField, Header("Weather Manager")]
        public RealtimeWeatherManager WeatherManager { get; private set; }

        [field: SerializeField, Header("Exploration Manager")]
        public ExplorationManager ExplorationManager { get; private set; }
    }
}
