namespace VUDK.Features.More.DialogueSystem.Components
{
    using UnityEngine;
    using VUDK.Features.Main.TriggerSystem;
    using VUDK.Features.More.DialogueSystem.Events;

    public class DSDialogueTrigger : DSDialogueSelectorBase, ITrigger
    {
        private bool _hasBeenTriggered;
        private bool _isSpeaking;

        private void OnEnable()
        {
            DSEvents.OnDMEnd += OnDialogueEnded;
        }

        private void OnDisable()
        {
            DSEvents.OnDMEnd -= OnDialogueEnded;
        }

#if UNITY_EDITOR
        [ContextMenu("Trigger Dialogue")]
#endif
        public virtual void Trigger()
        {
            if( (!IsRepeatable && _hasBeenTriggered) || _isSpeaking)
                return;

            _isSpeaking = true;
            _hasBeenTriggered = true;

            if (DialogueContainer == null || DialogueContainer.StartingDialogues.Count == 0)
                return;

            DSEvents.DialogueStartHandler?.Invoke(
                this, 
                new OnStartDialogueEventArgs(DialogueContainer, StartDialogue, RandomStartDialogue, IsInstantDialogue)
                );
        }

        public void Interrupt()
        {
            DSEvents.DialogueInterruptHandler?.Invoke();
        }

        private void OnDialogueEnded()
        {
            _isSpeaking = false;
        }
    }
}
