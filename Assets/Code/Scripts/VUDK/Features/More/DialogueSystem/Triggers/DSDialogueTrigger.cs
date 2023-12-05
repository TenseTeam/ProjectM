namespace VUDK.Features.More.DialogueSystem.Triggers
{
    using UnityEngine;
    using VUDK.Features.Main.TriggerSystem;
    using VUDK.Features.More.DialogueSystem.Events;
    using static VUDK.Features.More.DialogueSystem.Events.DSEvents;

    public class DSDialogueTrigger : DSDialogueBase, ITrigger
    {
        private bool _hasBeenTriggered;

#if UNITY_EDITOR
        [ContextMenu("Trigger Dialogue")]
#endif
        public virtual void Trigger()
        {
            if(!IsRepeatable && _hasBeenTriggered)
                return;

            _hasBeenTriggered = true;

            if (DialogueContainer == null || DialogueContainer.StartingDialogues.Count == 0)
                return;

            OnStartDialogue?.Invoke(
                this, 
                new OnStartDialogueEventArgs(DialogueContainer, StartDialogue, RandomStartDialogue, IsInstantDialogue)
                );
        }

        public void Interrupt()
        {
            OnDialogueInterrupt?.Invoke();
        }
    }
}
