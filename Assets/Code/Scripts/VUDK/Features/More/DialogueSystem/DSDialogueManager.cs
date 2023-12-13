namespace VUDK.Features.More.DialogueSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Enums;
    using VUDK.Features.More.DialogueSystem.Events;
    using VUDK.Features.More.DialogueSystem.UI;

    [RequireComponent(typeof(AudioSource))]
    public class DSDialogueManager : MonoBehaviour
    {
        [Header("Dialogue Settings")]
        [SerializeField, Min(0.01f)]
        private float _displayLetterTime = 0.1f;
        [SerializeField]
        private bool _hasCloseButton;

        [Header("UI Dialogue")]
        [SerializeField]
        private RectTransform _dialoguePanel;
        [SerializeField]
        private RectTransform _dialogueBox;
        [SerializeField]
        private Image _actorIconImage;
        [SerializeField]
        private TMP_Text _actorNameText;
        [SerializeField]
        private TMP_Text _dialogueText;

        [Header("UI Buttons")]
        [SerializeField]
        private Button _skipButton;
        [SerializeField]
        private Button _dialogueCloseButton;

        [Header("UI Choices")]
        [SerializeField]
        private RectTransform _choicesBox;
        [SerializeField]
        private bool _hideDialogueBoxOnChoice;
        [SerializeField]
        private bool _hasChoicePrefix = true;
        [SerializeField]
        private List<UIDSChoiceButton> _choiceButtons;

        private DSDialogueContainerData _dialogueContainerData;
        private DSDialogueData _currentDialogue;
        private AudioSource _audioSource;

        private bool _isWaitingForChoice;
        private bool _isSkipping;
        private bool _isDialogueEnded;
        private bool _isInstant;

        public bool IsDialogueOpen => _dialoguePanel.gameObject.activeSelf;
        public bool IsTalking { get; private set; }
        private bool _hasActor => _currentDialogue.ActorData != null;
        private int _maxChoices => _choiceButtons.Count;


        private void Awake()
        {
            TryGetComponent(out _audioSource);
            _audioSource.playOnAwake = false;
            Disable();
        }

        private void OnEnable()
        {
            DSEvents.DialogueStartHandler += StartDialogue;
            DSEvents.DialogueChoiceHandler += SelectChoice;
            DSEvents.DialogueInterruptHandler += InterruptDialogue;
            _skipButton.onClick.AddListener(NextDialogueInput);
            _dialogueCloseButton.onClick.AddListener(InterruptDialogue);
        }

        private void OnDisable()
        {
            DSEvents.DialogueStartHandler -= StartDialogue;
            DSEvents.DialogueChoiceHandler -= SelectChoice;
            DSEvents.DialogueInterruptHandler -= InterruptDialogue;
            _skipButton.onClick.RemoveListener(NextDialogueInput);
            _dialogueCloseButton.onClick.RemoveListener(InterruptDialogue);
        }

        public void StartDialogue(object sender, OnStartDialogueEventArgs args)
        {
            DSEvents.OnDMStart?.Invoke(_dialogueContainerData);
            DSEvents.OnDMAnyStart?.Invoke();
            _dialogueContainerData = args.DialogueContainerData;
            _isInstant = args.IsInstant;

            if (args.RandomStart || !args.DialogueData)
                _currentDialogue = RandomStartDialogue();
            else
                _currentDialogue = args.DialogueData;

            _isDialogueEnded = false;
            Enable();
            NextDialogue();
        }

        private void EndDialogue()
        {
            DSEvents.OnDMEnd?.Invoke(_dialogueContainerData);
            DSEvents.OnDMAnyEnd?.Invoke();
            _isInstant = false;
            _isDialogueEnded = true;
        }

        public void NextDialogue()
        {
            DSEvents.OnDMAnyNext?.Invoke();
            DSEvents.OnDMNext?.Invoke(_currentDialogue);
            DisplayActorInfo();
            PlayDialogueAudio();

            if (_isInstant)
                CompleteDialogueText(_currentDialogue.DialogueText);
            else
                StartPrintingDialogueText(_currentDialogue.DialogueText);

            if (!HasNextDialogue(_currentDialogue))
            {
                EndDialogue();
                return;
            }

            switch (_currentDialogue.DialogueType)
            {
                case DSDialogueType.SingleChoice:
                    _currentDialogue = _currentDialogue.Choices[0].NextDialogue;
                    break;

                case DSDialogueType.MultipleChoice:
                    BeginChoicePhase(_currentDialogue);
                    break;
            }
        }

        private void PlayDialogueAudio()
        {
            _audioSource.Stop();

            if (!_hasActor)
            {
                if (_currentDialogue.DialogueAudioClip)
                    _audioSource.PlayOneShot(_currentDialogue.DialogueAudioClip); // Doesn't use the AudioManager to be more modular
            }
            else
                _currentDialogue.ActorData.OnEmitDialogue?.Invoke(_currentDialogue.DialogueAudioClip);
        }

        public void Enable()
        {
            if (_hasCloseButton) EnableCloseButton();

            _dialoguePanel.gameObject.SetActive(true);
        }

        public void Disable()
        {
            if (_hasCloseButton) DisableCloseButton();

            DSEvents.OnDMDisable?.Invoke();
            _dialoguePanel.gameObject.SetActive(false);
            DisableChoicesBox();
        }

        public void EnableCloseButton()
        {
            _dialogueCloseButton.gameObject.SetActive(true);
        }

        public void DisableCloseButton()
        {
            _dialogueCloseButton.gameObject.SetActive(false);
        }

        public void EnableDialogueBox()
        {
            _dialogueBox.gameObject.SetActive(true);
        }

        public void DisableDialoguePane()
        {
            _dialogueBox.gameObject.SetActive(false);
        }

        private void DisableChoicesBox()
        {
            _isWaitingForChoice = false;
            _choicesBox.gameObject.SetActive(false);
        }

        private void EnableChoicesBox()
        {
            _choicesBox.gameObject.SetActive(true);
        }

        public void InterruptDialogue()
        {
            DSEvents.OnDMInterrupt?.Invoke(_currentDialogue);
            StopAllCoroutines();
            Disable();
        }

        private void BeginChoicePhase(DSDialogueData dialogueData)
        {
            if (_hideDialogueBoxOnChoice)
                DisableDialoguePane();

            _isWaitingForChoice = true;
            EnableChoicesBox();

            int choicesCount = Mathf.Min(dialogueData.Choices.Count, _maxChoices);

            Debug.Log($"Choices count: {choicesCount}");

            foreach (UIDSChoiceButton choiceButton in _choiceButtons) // Disable all buttons
                choiceButton.Disable();

            for (int i = 0; i < choicesCount; i++) // Enable only the buttons that will be used
            {
                _choiceButtons[i].Init(i);
                _choiceButtons[i].Enable();
                _choiceButtons[i].Text.text = _hasChoicePrefix ? $"{i+1}. {dialogueData.Choices[i].Text}" : dialogueData.Choices[i].Text;
            }
        }

        private void EndChoicePhase()
        {
            if (_hideDialogueBoxOnChoice)
                EnableDialogueBox();

            DisableChoicesBox();
        }

        private void NextDialogueInput()
        {
            if (!IsDialogueOpen) return;

            if (IsTalking)
            {
                _isSkipping = true;
            }
            else
            {
                if (_isDialogueEnded)
                {
                    Disable();
                    return;
                }

                if (!_isWaitingForChoice)
                    NextDialogue();
            }
        }

        private void SelectChoice(int choiceIndex)
        {
            if (_currentDialogue.Choices[choiceIndex].NextDialogue == null)
            {
                InterruptDialogue();
                return;
            }

            if (choiceIndex >= 0 && choiceIndex < _currentDialogue.Choices.Count)
            {
                _currentDialogue = _currentDialogue.Choices[choiceIndex].NextDialogue;
                EndChoicePhase();
                NextDialogue();
            }
            else
            {
                Debug.LogError($"Choice index {choiceIndex} is out of range.");
            }
        }

        private void DisplayActorInfo()
        {
            if (!_hasActor)
            {
                _actorIconImage.sprite = null;
                _actorNameText.text = "";
                return;
            }

            _actorIconImage.sprite = _currentDialogue.ActorData.ActorIcon;
            _actorNameText.text = _currentDialogue.ActorData.Name;
        }

        private DSDialogueData RandomStartDialogue()
        {
            if (_dialogueContainerData.StartingDialogues.Count == 0)
            {
                Debug.LogError("There is no starting dialogue in the dialogue container.");
            }

            return _dialogueContainerData.StartingDialogues[Random.Range(0, _dialogueContainerData.StartingDialogues.Count)];
        }

        private bool HasNextDialogue(DSDialogueData dialogueData)
        {
            return dialogueData != null && dialogueData.Choices.Count > 0 && dialogueData.Choices[0].NextDialogue != null;
        }

        private void StartPrintingDialogueText(string dialogueText)
        {
            StartCoroutine(PrintDialogueRoutine(dialogueText));
        }

        private void CompleteDialogueText(string dialogueText)
        {
            IsTalking = false;
            _isSkipping = false;
            _dialogueText.text = dialogueText;
            DSEvents.OnDMCompletedSentence?.Invoke();
        }

        private IEnumerator PrintDialogueRoutine(string dialogueText)
        {
            _dialogueText.text = "";
            IsTalking = true;
            _isSkipping = false;

            foreach (char letter in dialogueText.ToCharArray())
            {
                if (_isSkipping) break;

                _dialogueText.text += letter;
                yield return new WaitForSeconds(_displayLetterTime);
            }

            CompleteDialogueText(dialogueText);
        }
    }
}