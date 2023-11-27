namespace ProjectM.Features.UI.UIExploration
{
    using ProjectM.Features.ExplorationSystem.Nodes;
    using ProjectM.Managers;
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;

    public class ObservationPanel : MonoBehaviour, ICastGameManager<GameManager>
    {
        [SerializeField]
        private Button _button;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        public NodeBase CurrentNode => GameManager.ExplorationManager.CurrentTargetNode;

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
            if (CurrentNode is NodeInteractiveView nodeInteractiveView) nodeInteractiveView.InteractNextNode();
            gameObject.SetActive(false);
        }
    }
}