namespace ProjectM.Features.Puzzles.DialogueRewarder
{
    using ProjectM.Constants;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.Events;
    using UnityEngine.Localization;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Features.Main.PointsSystem.Rewards;
    using VUDK.Features.More.DialogueSystem.Data;
    using VUDK.Features.More.DialogueSystem.Events;
    using VUDK.Features.More.GameTaskSystem;

    [RequireComponent(typeof(RewardTrigger))]
    public class DialogueRewarder : GameTaskSaverBase
    {
        [Header("Dialogue Listener Settings")]
        [SerializeField]
        private LocalizedAsset<DSDialoguesPoolData> _dialoguesPool;
        [SerializeField]
        private bool _isInstant;

        private DSDialoguesPoolData _loadedDialogues;
        private DSDialogueContainerData _currentDialogueContainerData;
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

        private void OnEnable()
        {
            _dialoguesPool.AssetChanged += LoadDialoguesPool;
        }

        private void OnDisable()
        {
            _dialoguesPool.AssetChanged -= LoadDialoguesPool;
        }

        public void SubscribeEvents()
        {
            if (_currentDialogueContainerData == null) return;
            _currentDialogueContainerData.OnStart += OnDialogueStart;
            _currentDialogueContainerData.OnEnd += OnDialogueEnd;
            _currentDialogueContainerData.OnNext += OnDialogueNext;
            _currentDialogueContainerData.OnInterrupt += OnDialogueInterrupt;
        }

        public void UnsubscribeEvents()
        {
            if(_currentDialogueContainerData == null) return;
            _currentDialogueContainerData.OnStart -= OnDialogueStart;
            _currentDialogueContainerData.OnEnd -= OnDialogueEnd;
            _currentDialogueContainerData.OnNext -= OnDialogueNext;
            _currentDialogueContainerData.OnInterrupt -= OnDialogueInterrupt;
        }

        public override void BeginTask()
        {
            base.BeginTask();
            if(IsSolved && !IsRepeatable) return;
            _currentDialogueContainerData = _loadedDialogues.Pool[Random.Range(0, _loadedDialogues.Pool.Count)];
            DSEventsHandler.StartDialogue(this, _currentDialogueContainerData, null, true, _isInstant);
            EventManager.Ins.TriggerEvent(GameEventKeys.OnDialogueRewarderBeginTask);
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

        private void LoadDialoguesPool(DSDialoguesPoolData asset)
        {
            _loadedDialogues = asset;
        }
    }
}