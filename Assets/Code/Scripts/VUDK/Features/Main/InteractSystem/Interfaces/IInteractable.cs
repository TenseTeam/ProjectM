namespace VUDK.Features.Main.InteractSystem.Interfaces
{
    using UnityEngine;

    public interface IInteractable
    {
        /// <summary>
        /// Interacts with this object.
        /// </summary>
        public void Interact();

        public void EnableInteraction();

        public void DisableInteraction();
    }
}