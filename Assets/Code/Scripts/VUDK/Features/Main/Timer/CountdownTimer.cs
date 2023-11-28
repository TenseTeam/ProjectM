namespace VUDK.Features.Main.Timer
{
    using System.Collections;
    using UnityEngine;
    using VUDK.Config;
    using VUDK.Generic.Managers.Main;

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
