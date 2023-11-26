namespace ProjectM.Features.UI.UIExploration
{
    using ProjectM.Constants;
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;

    public class ObservationPanel : MonoBehaviour
    {
        [SerializeField]
        private Button _button;

        private void OnEnable()
        {
            _button.onClick.AddListener(Disable);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(Disable);
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnExitButtonViewArt);
            gameObject.SetActive(false);
        }
    }
}