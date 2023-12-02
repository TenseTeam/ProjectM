namespace VUDK.Features.Main.Timer
{
    using System.Collections;
    using UnityEngine;
    using VUDK.Constants;
    using VUDK.Features.Main.EventSystem;

    public class CountdownTimer : MonoBehaviour
    {
        public void StartTimer(int time)
        {
            StartCoroutine(CountdownRoutine(time));
        }

        public void StopTimer()
        {
            StopAllCoroutines();
        }

        private IEnumerator CountdownRoutine(int time)
        {
            do
            {
                EventManager.Ins.TriggerEvent(EventKeys.CountdownEvents.OnCountdownCount, time);
                yield return new WaitForSeconds(1);
                time--;
            } while (time > 0);

            EventManager.Ins.TriggerEvent(EventKeys.CountdownEvents.OnCountdownCount, time);
            EventManager.Ins.TriggerEvent(EventKeys.CountdownEvents.OnCountdownTimesUp);
        }
    }
}
