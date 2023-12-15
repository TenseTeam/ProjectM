namespace VUDK.Features.More.DateTaskSystem
{
    using System;
    using UnityEngine;
    using VUDK.Features.Main.SaveSystem.Bases;
    using VUDK.Features.More.DateTaskSystem.Data;
    using VUDK.Patterns.Initialization.Interfaces;

    public class DateTask : BinarySaverBase<DateTaskData>, IInit
    {
        private enum TaskCycle
        {
            Daily,
            Weekly,
            Monthly,
            Yearly
        }

        [Header("Date Task Settings")]
        [SerializeField]
        private TaskCycle _taskCycle;

        public bool HasCompleted => CheckHasCompleted();
        public DateTime? LastDate => SaveData.LastDataCompleted;

        public void Init()
        {
            Load(nameof(DateTask) + GetInstanceID(), ".save");
        }

        public bool Check()
        {
            return SaveData != null;
        }

        public void CompleteTask()
        {
            SaveData.LastDataCompleted = DateTime.Now;
            Save(nameof(DateTask) + GetInstanceID(), ".save");
        }

        protected override void OnLoadFail()
        {
            SaveData = new DateTaskData();
        }

        private bool CheckHasCompleted()
        {
            if(LastDate == null) return false;

            DateTime currentDate = DateTime.Now;

            switch (_taskCycle)
            {
                case TaskCycle.Daily:
                    return currentDate.Date <= LastDate?.Date.AddDays(1);
                case TaskCycle.Weekly:
                    return currentDate.Date <= LastDate?.Date.AddDays(7);
                case TaskCycle.Monthly:
                    return currentDate.Date <= LastDate?.Date.AddMonths(1);
                case TaskCycle.Yearly:
                    return currentDate.Date <= LastDate?.Date.AddYears(1);
            }

            return false;
        }
    }
}