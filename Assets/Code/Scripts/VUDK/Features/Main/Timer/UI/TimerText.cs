namespace VUDK.Features.Main.Timer.UI
{
    using UnityEngine;
    using TMPro;
    using VUDK.Constants;
    using VUDK.Features.Main.EventSystem;

    public class TimerText : MonoBehaviour
    {
        [SerializeField]
        private string _incipit;

        [SerializeField]
        private TMP_Text _text;

        private void OnEnable()
        {
            EventManager.Ins.AddListener<int>(EventKeys.CountdownEvents.OnCountdownCount, UpdateTimerText);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<int>(EventKeys.CountdownEvents.OnCountdownCount, UpdateTimerText);
        }

        private void UpdateTimerText(int time)
        {
            _text.text = _incipit + time.ToString();
        }
    }
}