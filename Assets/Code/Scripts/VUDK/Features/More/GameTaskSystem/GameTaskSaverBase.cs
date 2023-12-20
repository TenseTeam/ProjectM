namespace VUDK.Features.More.GameTaskSystem
{
    using System;
    using UnityEngine;
    using VUDK.Features.Main.SaveSystem;
    using VUDK.Features.Main.SaveSystem.Interfaces;
    using VUDK.Features.Main.SaveSystem.SaveData;
    using VUDK.Features.Main.TimerSystem.Events;
    using VUDK.Features.More.GameTaskSystem.Bases;
    using VUDK.Features.More.GameTaskSystem.SaveData;
    using VUDK.Patterns.Initialization.Interfaces;

    public abstract class GameTaskSaverBase<T> : GameTaskBase, ISavable, IInit where T : TaskSaveValue, new()
    {
        [Header("Task Settings")]
        [SerializeField]
        private int _taskPeriod = 24;

        protected T SaveValue;
        public int SaveID => GetInstanceID();
        public DateTime AchieveDate => SaveValue.LastCompletedTime.AddHours(_taskPeriod);

        protected virtual void Awake()
        {
            Pull();
            Init();
        }

        public virtual void Init()
        {
            IsInProgress = SaveValue.IsInProgress;
            IsSolved = !IsTimePassed();
        }

        public bool Check()
        {
            return SaveValue != null;
        }

        public virtual string GetSaveName() => "Tasks";

        private bool IsTimePassed()
        {
            return DateTime.Now > AchieveDate;
        }

        public override void BeginTask()
        {
            base.BeginTask();
            Push(); // Push to save task is in progress
        }

        public override void ResolveTask()
        {
            base.ResolveTask();
            SaveValue.IsInProgress = IsInProgress;
            SaveValue.LastCompletedTime = DateTime.Now;
            DisplayTimer(true);
            Push(); // Push to save task is solved
        }

        public void Push()
        {
            SavePacketData saveData = new SavePacketData(SaveValue);
            SaveManager.Push(GetSaveName(), SaveID, saveData);
        }

        public void Pull()
        {
            if (SaveManager.TryPull<T>(GetSaveName(), SaveID, out SavePacketData _saveData))
            {
                SaveValue = _saveData.Value as T;
            }
            else
            {
                SaveValue = new T();
            }
        }

        protected override void OnEnterFocus()
        {
            IsSolved = !IsTimePassed();
            base.OnEnterFocus();
        }

        protected override void OnExitFocus()
        {
            base.OnExitFocus();
            DisplayTimer(false);
        }

        protected int GetSecondsToWait()
        {
            TimeSpan timeDifference = AchieveDate - DateTime.Now;
            int secondsToWait = (int)timeDifference.TotalSeconds;
            return secondsToWait;
        }

        protected void DisplayTimer(bool isEnabled)
        {
            if (isEnabled)
                TimerEventsHandler.StartTimerHandler(GetSecondsToWait());
            else
                TimerEventsHandler.StopTimerHandler();
        }

        protected override void OnEnterFocusIsSolved()
        {
            DisplayTimer(true);
        }
    }

    public abstract class GameTaskSaverBase : GameTaskSaverBase<TaskSaveValue>
    {
    }
}