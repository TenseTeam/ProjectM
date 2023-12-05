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
        public static class Errors
        {
            public const int APIInvalid = 401;
            public const int APIExceededCallsPerMonthQuota = 403;
            public const int InvalidQueryOrURL = 400;
            public const int TooManyRequests = 429;
        }

        public static void GetWeather(APIPackageData apipackage, string query, Action<WeatherData> onReceivedWeatherData, Action onFailedToReceiveWeatherData)
        {
            GetWeather(apipackage.APIS, query, onReceivedWeatherData, onFailedToReceiveWeatherData);
        }

        public static void GetWeather(string[] apikeys, string query, Action<WeatherData> onReceivedWeatherData, Action onFailedToReceivedWeatherData)
        {
            string url = $"https://weatherapi-com.p.rapidapi.com/current.json?q={query}";

            UnityWebRequest request = UnityWebRequest.Get(url);

            if (apikeys.Length == 0)
            {
                Debug.LogWarning("No Valid API-KEYS found.");
                onFailedToReceivedWeatherData?.Invoke();
                return;
            }

            request.SetRequestHeader("X-RapidAPI-Key", apikeys[0]);
            request.SetRequestHeader("X-RapidAPI-Host", "weatherapi-com.p.rapidapi.com");

            var operation = request.SendWebRequest();

            operation.completed += (AsyncOperation obj) =>
            {
                if (request.result == UnityWebRequest.Result.ConnectionError || request.result == UnityWebRequest.Result.ProtocolError)
                {
                    if( request.responseCode == Errors.APIInvalid || 
                        request.responseCode == Errors.APIExceededCallsPerMonthQuota || 
                        request.responseCode == Errors.TooManyRequests)
                    {
                        Debug.LogWarning($"Error with API-KEY {apikeys[0]}: {request.error}.\n Trying Next API-KEY.");
                        GetWeather(apikeys.Skip(1).ToArray(), query, onReceivedWeatherData, onFailedToReceivedWeatherData);
                        return;
                    }

                    Debug.LogWarning($"Error: {request.error}");
                    onFailedToReceivedWeatherData?.Invoke();
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