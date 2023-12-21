namespace ProjectM.Utility
{
    using UnityEngine;

    public class GameObjectActivator : MonoBehaviour
    {
        public GameObject GameObject;

        /// <summary>
        /// Enables the GameObject.
        /// </summary>
        public void EnableGameObject()
        {
            GameObject.SetActive(true); 
        }

        /// <summary>
        /// Disables the GameObject.
        /// </summary>
        public void DisableGameObject()
        {
            GameObject.SetActive(false);
        }
    }
}
