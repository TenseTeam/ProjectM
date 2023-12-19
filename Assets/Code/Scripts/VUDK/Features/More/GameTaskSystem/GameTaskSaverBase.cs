namespace VUDK.Features.More.GameTaskSystem
{
    using System;
    using UnityEngine;
    using VUDK.Features.Main.SaveSystem;
    using VUDK.Features.Main.SaveSystem.SaveData;
    using VUDK.Features.Main.SaveSystem.Interfaces;
    using VUDK.Features.More.GameTaskSystem.Bases;
    using VUDK.Features.More.GameTaskSystem.SaveData;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Features.Main.TimerSystem.Events;

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

        private bool IsTimePassed()
        {
            return DateTime.Now > AchieveDate;
        }

        public override void BeginTask()
        {
            base.BeginTask();
            IsSolved = !IsTimePassed();
            if (IsSolved && !IsRepeatable)
                TimerEventsHandler.StartTimerHandler(GetSecondsToWait());
            Push(); // Push to save task is in progress
        }

        public override void ResolveTask()
        {
            base.ResolveTask();
            SaveValue.IsInProgress = IsInProgress;
            SaveValue.LastCompletedTime = DateTime.Now;
            Push(); // Push to save task is solved
        }

        public override void OnExitFocus()
        {
            base.OnExitFocus();
            TimerEventsHandler.StopTimerHandler();
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
                SaveValue =  _saveData.Value as T;
            }
            else
            {
                SaveValue = new T();
            }
        }

        public string GetSaveName() => "Tasks";

        protected int GetSecondsToWait()
        {
            TimeSpan timeDifference = AchieveDate - DateTime.Now;
            int secondsToWait = (int)timeDifference.TotalSeconds;
            return secondsToWait;
        }
    }

    public abstract class GameTaskSaverBase : GameTaskSaverBase<TaskSaveValue>
    {
    }
}
