namespace VUDK.Generic.Managers.Main.Bases
{
    using UnityEngine;
    using VUDK.Config;
    using VUDK.Patterns.StateMachine;

    [DefaultExecutionOrder(-990)]
    public abstract class GameMachineBase : StateMachine
    {
        public override void Init()
        {
#if UNITY_EDITOR
            Debug.Log("GameStateMachine initialized.");
#endif
            MainManager.Ins.EventManager.TriggerEvent(EventKeys.GameEvents.OnGameMachineStart);
        }
    }
}
