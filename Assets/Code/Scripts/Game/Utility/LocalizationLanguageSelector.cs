namespace ProjectM.Utility
{
    using UnityEngine;
    using UnityEngine.Localization.Settings;

    public class LocalizationLanguageSelector : MonoBehaviour
    {
        public void SetLanguage(string language)
        {
            LocalizationSettings.SelectedLocale = LocalizationSettings.AvailableLocales.GetLocale(language);
        }
    }
}