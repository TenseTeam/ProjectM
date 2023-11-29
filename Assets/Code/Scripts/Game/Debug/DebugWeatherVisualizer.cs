namespace ProjectM.Debug
{
    using TMPro;
    using UnityEngine;
    using VUDK.Constants;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.More.WeatherSystem.Data;

    [RequireComponent(typeof(TMP_Text))]
    public class DebugWeatherVisualizer : MonoBehaviour
    {
        private TMP_Text _text;

        private void Awake()
        {
            TryGetComponent(out _text);
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener<WeatherData>(EventKeys.WeatherEvents.OnWeatherChanged, OnWeatherChanged);
        }

        private void OnWeatherChanged(WeatherData data)
        {
            _text.text = $"Weather:\n{data}";
        }
    }
}