namespace ProjectM.Features.Puzzles.Quiz.UI
{
    using ProjectM.Constants;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.More.DialogueSystem.Events;
    using VUDK.Patterns.Initialization.Interfaces;

    public class UIQuizAnswerButton : MonoBehaviour, IInit<int>
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
            Button.onClick.AddListener(SelectAnswer);
        }

        private void OnDisable()
        {
            Button.onClick.RemoveListener(SelectAnswer);
        }

        /// <inheritdoc/>
        public void Init(int choiceIndex)
        {
            _choiceIndex = choiceIndex;
        }

        /// <inheritdoc/>
        public bool Check()
        {
            return Button != null && Text != null;
        }

        /// <summary>
        /// Enables the button.
        /// </summary>
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Disables the button.
        /// </summary>
        public void Disable()
        {
            gameObject.SetActive(false);
        }

        /// <summary>
        /// Selects the answer.
        /// </summary>
        private void SelectAnswer()
        {
            EventManager.Ins.TriggerEvent(GameEventKeys.OnSelectQuizAnswer, _choiceIndex);
        }
    }
}