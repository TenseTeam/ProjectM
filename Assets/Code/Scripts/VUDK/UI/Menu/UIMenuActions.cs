namespace VUDK.UI.Menu
{
    using UnityEngine;
    using VUDK.Config;
    using VUDK.Generic.Managers.Main;

    public class UIMenuActions : MonoBehaviour
    {
        private void Start()
        {
            EventManager.Ins.TriggerEvent(EventKeys.SceneEvents.OnMainMenuLoaded);
        }

        public void WaitChangeScene(int sceneIndex)
        {
            ClickButton();
            MainManager.Ins.SceneManager.WaitChangeScene(sceneIndex);
        }

        public void ExitApplication()
        {
            ClickButton();
            Application.Quit();
        }

        public void ClickButton()
        {
            EventManager.Ins.TriggerEvent(EventKeys.UIEvents.OnButtonPressed);
        }
    }
}