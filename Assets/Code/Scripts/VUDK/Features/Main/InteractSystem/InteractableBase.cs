namespace VUDK.Features.Main.InteractSystem
{
    using UnityEngine;
    using VUDK.Features.Main.InteractSystem.Interfaces;

    public abstract class InteractableBase : MonoBehaviour, IInteractable
    {
        public abstract void DisableInteraction();

        public abstract void EnableInteraction();

        public abstract void Interact();
    }
}
