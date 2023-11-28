namespace ProjectM.Features.ExplorationSystem.Transition.Phases
{
    using System;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.StateMachine;
    using ProjectM.Constants;
    using ProjectM.Features.ExplorationSystem.Nodes;

    public class TransitionEnd : TransitionPhaseBase
    {
        public TransitionEnd(Enum stateKey, StateMachine relatedStateMachine, StateMachineContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnEndTransition);
            Context.Transition.End();
            Context.TargetNode.OnNodeEnter();
        }

        public override void Process()
        {
        }

        public override void FixedProcess()
        {
        }

        public override void Exit()
        {
        }

        private void CheckCameraEnable()
        {
        }
    }
}
