namespace VUDK.Features.More.DialogueSystem.Triggers
{
    using UnityEngine;
    using VUDK.Features.Main.TriggerSystem;
    using VUDK.Features.More.DialogueSystem.Events;

    public class DSDialogueTrigger : DSDialogueBase, ITrigger
    {
#if UNITY_EDITOR
        [ContextMenu("Trigger this dialogue")]
#endif
        public virtual void Trigger()
        {
            if (DialogueContainer == null
            || DialogueContainer.StartingDialogues.Count == 0) return;

            DSEvents.OnStartDialogue?.Invoke(DialogueContainer, Dialogue, RandomStartDialogue);
        }
    }
}
