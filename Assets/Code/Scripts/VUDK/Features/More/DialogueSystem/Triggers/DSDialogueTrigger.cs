namespace VUDK.Features.More.DialogueSystem.Triggers
{
    using UnityEngine;
    using VUDK.Features.Main.TriggerSystem;
    using VUDK.Features.More.DialogueSystem.Events;
    using static VUDK.Features.More.DialogueSystem.Events.DSEvents;

    public class DSDialogueTrigger : DSDialogueBase, ITrigger
    {
#if UNITY_EDITOR
        [ContextMenu("Trigger this dialogue")]
#endif
        public virtual void Trigger()
        {
            if (DialogueContainer == null
            || DialogueContainer.StartingDialogues.Count == 0) return;

            OnStartDialogue?.Invoke(
                this, 
                new OnStartDialogueEventArgs(DialogueContainer, StartDialogue, RandomStartDialogue, IsInstantDialogue)
                );
        }
    }
}
