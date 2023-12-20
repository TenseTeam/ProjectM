namespace ProjectM.Utility
{
    using UnityEngine;

    public class GameObjectActivator : MonoBehaviour
    {
        public GameObject GameObject;

        public void EnableGameObject()
        {
            GameObject.SetActive(true); 
        }

        public void DisableGameObject()
        {
            GameObject.SetActive(false);
        }
    }
}
