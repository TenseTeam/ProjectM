namespace VUDK.Features.More.DialogueSystem
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Features.More.DialogueSystem.Events;

    [RequireComponent(typeof(Button))]
    public class DSChoiceButton : MonoBehaviour, IInject<int>
    {
        [field: SerializeField]
        public Button Button { get; private set; }
        [field: SerializeField]
        public TMP_Text Text { get; private set; }

        private int _choiceIndex;

        private void OnValidate()
        {
            TryGetComponent(out Button button);
            Text = GetComponentInChildren<TMP_Text>();
            Button = button;
        }

        private void OnEnable()
        {
            Button.onClick.AddListener(SelectChoice);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(SelectChoice);
        }

        public void Inject(int choiceIndex)
        {
            _choiceIndex = choiceIndex;
        }

        private void SelectChoice()
        {
            DSEvents.OnSelectedChoice?.Invoke(_choiceIndex);
        }
    }
}