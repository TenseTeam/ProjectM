namespace ProjectM.Features.UI.UIExploration
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.Packages.ExplorationSystem.Constants;
    using VUDK.Features.Main.EventSystem;

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
            EventManager.Ins.AddListener(ExplorationEventKeys.OnEnterNodeObservation, EnterObservation);
            EventManager.Ins.AddListener(ExplorationEventKeys.OnExitNodeObservation, ExitObservation);
            _exitViewButton.onClick.AddListener(OnExitObservationButtonHanlder);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener(ExplorationEventKeys.OnEnterNodeObservation, EnterObservation);
            EventManager.Ins.RemoveListener(ExplorationEventKeys.OnExitNodeObservation, ExitObservation);
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
            EventManager.Ins.TriggerEvent(ExplorationEventKeys.OnExitObservationButton);
        }
    }
}