﻿namespace VUDK.Features.More.DialogueSystem
{
    using System.Collections;
    using System.Collections.Generic;
    using TMPro;
    using UnityEngine;
    using UnityEngine.InputSystem;
    using UnityEngine.UI;
    using VUDK.Features.Main.InputSystem;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Enums;
    using VUDK.Features.More.DialogueSystem.Events;

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
        [SerializeField]
        private Button _dialogueCloseButton;

        [Header("UI Choices")]
        [SerializeField]
        private RectTransform _choicesPanel;
        [SerializeField]
        private bool _hideDialogueBoxOnChoice;
        [SerializeField]
        private List<DSChoiceButton> _choiceButtons;

        private DSDialogueContainerData _dialogueContainerData;
        private DSDialogueData _currentDialogue;

        private bool _isWaitingForChoice;
        private bool _isSkipping;
        private bool _isDialogueEnded;
        private bool _isInstant;

        public bool IsDialogueOpen => _dialoguePanel.gameObject.activeSelf;
        public bool IsTalking { get; private set; }
        private int _maxChoices => _choiceButtons.Count;

        private void Awake() => Disable();

        private void OnEnable()
        {
            DSEvents.OnStartDialogue += StartDialogue;
            DSEvents.OnEndDialogue += EndDialogue;
            DSEvents.OnDialogueChoice += SelectChoice;
            DSEvents.OnDialogueInterrupt += EndDialogueAndDisable;
            InputsManager.Inputs.Dialogue.SkipSentence.canceled += InputNextDialogueText;
            _dialogueCloseButton.onClick.AddListener(EndDialogueAndDisable);
        }

        private void OnDisable()
        {
            DSEvents.OnStartDialogue -= StartDialogue;
            DSEvents.OnEndDialogue -= EndDialogue;
            DSEvents.OnDialogueChoice -= SelectChoice;
            DSEvents.OnDialogueInterrupt -= EndDialogueAndDisable;
            InputsManager.Inputs.Dialogue.SkipSentence.canceled -= InputNextDialogueText;
            _dialogueCloseButton.onClick.RemoveListener(EndDialogueAndDisable);
        }

        public void StartDialogue(object sender, OnStartDialogueEventArgs args)
        {
            bool randomStartDialogue = args.RandomStart;
            DSDialogueData firstDialogue = args.DialogueData;

            _dialogueContainerData = args.DialogueContainerData;
            _isInstant = args.IsInstant;

            if (randomStartDialogue)
                _currentDialogue = RandomStartDialogue();
            else
                _currentDialogue = firstDialogue;

            _isDialogueEnded = false;
            Enable();
            DisplayNextDialogue();
        }

        private void EndDialogue()
        {
            _isInstant = false;
            _isDialogueEnded = true;
        }

        public void DisplayNextDialogue()
        {
            SetDialogueActor(_currentDialogue.ActorData);

            if (_isInstant)
                PrintCompleteDialogueText(_currentDialogue.DialogueText);
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
                    WaitForChoice(_currentDialogue);
                    break;
            }
        }

        public void Enable()
        {
            if (_hasCloseButton) EnableCloseButton();

            _dialoguePanel.gameObject.SetActive(true);
        }

        public void Disable()
        {
            if (_hasCloseButton) DisableCloseButton();

            DisableChoices();
            _dialoguePanel.gameObject.SetActive(false);
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

        public void DisableDialogueBox()
        {
            _dialogueBox.gameObject.SetActive(false);
        }

        public void EndDialogueAndDisable()
        {
            StopAllCoroutines();
            EndDialogue();
            Disable();
        }

        private void EnableChoices()
        {
            if (_hideDialogueBoxOnChoice)
                DisableDialogueBox();

            _isWaitingForChoice = true;
            _choicesPanel.gameObject.SetActive(true);
        }

        private void DisableChoices()
        {
            if (_hideDialogueBoxOnChoice)
                EnableDialogueBox();

            _isWaitingForChoice = false;
            _choicesPanel.gameObject.SetActive(false);
        }

        private void InputNextDialogueText(InputAction.CallbackContext context)
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
                    DisplayNextDialogue();
            }
        }

        private void WaitForChoice(DSDialogueData dialogueData)
        {
            EnableChoices();
            DisplayChoices(dialogueData);
        }

        private void DisplayChoices(DSDialogueData dialogueData)
        {
            for (int i = 0; i < _maxChoices; i++)
            {
                _choiceButtons[i].Text.text = dialogueData.Choices[i].Text;
                _choiceButtons[i].Inject(i);
            }
        }

        private void SelectChoice(object sender, OnDialogueChoiceEventArgs args)
        {
            int choiceIndex = args.ChoiceIndex;

            if (_currentDialogue.Choices[choiceIndex].NextDialogue == null)
            {
                EndDialogueAndDisable();
                return;
            }

            if (choiceIndex >= 0 && choiceIndex < _currentDialogue.Choices.Count)
            {
                _currentDialogue = _currentDialogue.Choices[choiceIndex].NextDialogue;
                DisableChoices();
                DisplayNextDialogue();
            }
            else
            {
                Debug.LogError($"Choice index {choiceIndex} is out of range.");
            }
        }

        private void SetDialogueActor(DSActorData actorData)
        {
            if (actorData == null)
            {
                _actorIconImage.sprite = null;
                _actorNameText.text = "";
                return;
            }

            _actorIconImage.sprite = actorData.ActorIcon;
            _actorNameText.text = actorData.Name;
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

        private void PrintCompleteDialogueText(string dialogueText)
        {
            IsTalking = false;
            _isSkipping = false;
            _dialogueText.text = dialogueText;
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

            PrintCompleteDialogueText(dialogueText);
        }
    }
}