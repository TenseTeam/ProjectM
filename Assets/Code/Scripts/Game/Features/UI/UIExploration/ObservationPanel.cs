namespace ProjectM.Features.UI.UIExploration
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.FactoryMethod;
    using ProjectM.Features.ExplorationSystem;
    using ProjectM.Constants;

    public class ObservationPanel : MonoBehaviour
    {
        [Header("Panel Settings")]
        [SerializeField]
        private RectTransform _panel;
        [SerializeField]
        private Button _exitViewButton;

        private void Awake()
        {
            Disable();
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnEnterNodeObservation, EnterObservation);
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnExitNodeObservation, ExitObservation);
            _exitViewButton.onClick.AddListener(OnExitObservationButtonHanlder);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnEnterNodeObservation, EnterObservation);
            MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnExitNodeObservation, ExitObservation);
            _exitViewButton.onClick.RemoveListener(OnExitObservationButtonHanlder);
        }

        public void Enable()
        {
            _panel.gameObject.SetActive(true);
        }

        public void Disable()
        {
            _panel.gameObject.SetActive(false);
        }

        private void EnterObservation()
        {
            Enable();
        }

        private void ExitObservation()
        {
            Disable();
        }

        private void OnExitObservationButtonHanlder()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnExitObservationButton);
        }
    }
}