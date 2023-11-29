namespace VUDK.Features.Main.DialogueSystem
{
    using UnityEngine;
    using VUDK.Constants;
    using VUDK.Features.Main.EventSystem;

    public class DialogueTrigger : MonoBehaviour
    {
        [Header("Dialogue")]
        [SerializeField]
        private Dialogue _dialogue;

        public void Trigger()
        {
            EventManager.Ins.TriggerEvent(EventKeys.DialogueEvents.OnTriggeredDialouge, _dialogue);
        }

        public void Interrupt()
        {
            EventManager.Ins.TriggerEvent(EventKeys.DialogueEvents.OnInterruptDialogue);
        }
    }
}
