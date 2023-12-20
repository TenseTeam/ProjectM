namespace ProjectM.Features.Puzzles.Quiz.UI
{
    using ProjectM.Constants;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.More.DialogueSystem.Events;

    public class UIQuizAnswerButton : MonoBehaviour
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

        public void Init(int choiceIndex)
        {
            _choiceIndex = choiceIndex;
        }

        public bool Check()
        {
            return Button != null && Text != null;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }

        private void SelectAnswer()
        {
            EventManager.Ins.TriggerEvent(GameEventKeys.OnSelectQuizAnswer, _choiceIndex);
        }
    }
}