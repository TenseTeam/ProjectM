namespace ProjectM.Managers
{
    using UnityEngine;
    using VUDK.Features.More.WeatherSystem;
    using VUDK.Generic.Managers.Main.Bases;
    using ProjectM.Features.PathSystem;

    public class GameManager : GameManagerBase
    {
        [field: SerializeField, Header("Weather Manager")]
        public RealtimeWeatherManager WeatherManager { get; private set; }

        [field: SerializeField, Header("Path Manager")]
        public PathManager PathManager { get; private set; }
    }
}
