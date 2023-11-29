namespace ProjectM.Managers
{
    using UnityEngine;
    using VUDK.Features.Main.InputSystem.MobileInputs;
    using VUDK.Features.More.WeatherSystem;
    using VUDK.Generic.Managers.Main.Bases;
    using VUDK.Features.Main.DialogueSystem;
    using ProjectM.Features.Exploration;

    public class GameManager : GameManagerBase
    {
        [field: SerializeField, Header("Mobile Inputs Manager")]
        public MobileInputsManager MobileInputsManager { get; private set; }

        [field: SerializeField, Header("Weather Manager")]
        public RealtimeWeatherManager WeatherManager { get; private set; }

        [field: SerializeField, Header("Game Exploration")]
        public GameExplorationManager GameExploration { get; private set; }
    }
}
