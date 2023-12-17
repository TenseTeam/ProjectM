namespace VUDK.Features.More.DateTaskSystem.Data
{
    using System;
    using VUDK.Features.Main.SaveSystem.Data;

    [Serializable]
    public class DateTaskData : SaveData
    {
        public DateTime? LastDataCompleted;

        public DateTaskData()
        {
            LastDataCompleted = null;
        }
    }
}