namespace VUDK.Features.More.DialogueSystem.UI
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Features.More.DialogueSystem.Events;
    using VUDK.Patterns.Initialization.Interfaces;

    [RequireComponent(typeof(Button))]
    public class UIDSChoiceButton : MonoBehaviour, IInject<int>
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

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        private void SelectChoice()
        {
            DSEvents.DialogueChoiceHandler?.Invoke(this, new OnDialogueChoiceEventArgs(_choiceIndex));
        }
    }
}