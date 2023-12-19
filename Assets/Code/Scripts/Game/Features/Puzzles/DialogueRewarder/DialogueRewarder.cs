namespace ProjectM.Features.Puzzles.DialogueRewarder
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using VUDK.Features.Main.PointsSystem.Rewards;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Events;
    using VUDK.Features.More.GameTaskSystem;

    [RequireComponent(typeof(RewardTrigger))]
    public class DialogueRewarder : GameTaskSaverBase
    {
        [Header("Dialogue Listener Settings")]
        [SerializeField]
        private List<DSDialogueContainerData> _possibleDialogues;
        [SerializeField]
        private bool _isInstant;

        private DSDialogueContainerData _dialogueContainerData;
        private RewardTrigger _rewarder;

        [Header("Dialogue Events")]
        public UnityEvent OnStart;
        public UnityEvent OnEnd;
        public UnityEvent OnNext;
        public UnityEvent OnInterrupt;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _rewarder);
        }

        public void SubscribeEvents()
        {
            if (_dialogueContainerData == null) return;
            _dialogueContainerData.OnStart += OnDialogueStart;
            _dialogueContainerData.OnEnd += OnDialogueEnd;
            _dialogueContainerData.OnNext += OnDialogueNext;
            _dialogueContainerData.OnInterrupt += OnDialogueInterrupt;
        }

        public void UnsubscribeEvents()
        {
            if(_dialogueContainerData == null) return;
            _dialogueContainerData.OnStart -= OnDialogueStart;
            _dialogueContainerData.OnEnd -= OnDialogueEnd;
            _dialogueContainerData.OnNext -= OnDialogueNext;
            _dialogueContainerData.OnInterrupt -= OnDialogueInterrupt;
        }

        public override void BeginTask()
        {
            base.BeginTask();
            if(IsSolved && !IsRepeatable) return;
            _dialogueContainerData = _possibleDialogues[Random.Range(0, _possibleDialogues.Count)];
            DSEventsHandler.StartDialogue(this, _dialogueContainerData, null, true, _isInstant);
            SubscribeEvents();
        }

        public override void InterruptTask()
        {
            base.InterruptTask();
            DSEventsHandler.InterruptDialogue();
            UnsubscribeEvents();
        }

        public override void ResolveTask()
        {
            base.ResolveTask();
            _rewarder.TriggerReward();
        }

        protected virtual void OnDialogueStart()
        {
            OnStart?.Invoke();
        }

        protected virtual void OnDialogueNext()
        {
            OnNext?.Invoke();
        }

        protected virtual void OnDialogueInterrupt()
        {
            OnInterrupt?.Invoke();
        }

        protected virtual void OnDialogueEnd()
        {
            OnEnd?.Invoke();
            ResolveTask();
            UnsubscribeEvents();
        }
    }
}