namespace VUDK.Features.More.WeatherSystem
{
    using System;
    using System.Linq;
    using Newtonsoft.Json;
    using UnityEngine;
    using UnityEngine.Networking;
    using VUDK.Features.More.APISystem.Data;
    using VUDK.Features.More.WeatherSystem.Data;

    public static class WeatherCalculator
    {
        public static void GetWeather(APIPackageData apipackage, string query, Action<WeatherData> onReceivedWeatherData)
        {
            GetWeather(apipackage.APIS, query, onReceivedWeatherData);
        }

        public static void GetWeather(string[] apikeys, string query, Action<WeatherData> onReceivedWeatherData)
        {
            string url = $"https://weatherapi-com.p.rapidapi.com/current.json?q={query}";

            UnityWebRequest request = UnityWebRequest.Get(url);

            if(apikeys.Length == 0)
            {
                Debug.LogError("Invalid API-KEYS");
                return;
            }

            request.SetRequestHeader("X-RapidAPI-Key", apikeys[0]);
            request.SetRequestHeader("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");

            var operation = request.SendWebRequest();

            operation.completed += (AsyncOperation obj) =>
            {
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    Debug.LogWarning($"Error with API-KEY {apikeys[0]}: {request.error}.\n Trying Next API-KEY.");
                    GetWeather(apikeys.Skip(1).ToArray(), query, onReceivedWeatherData);
                }
                else
                {
                    string body = request.downloadHandler.text;
                    WeatherData weatherData = JsonConvert.DeserializeObject<WeatherData>(body);
                    onReceivedWeatherData?.Invoke(weatherData);
                }
            };
        }
    }
}