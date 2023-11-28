﻿namespace ProjectM.Features.ExplorationSystem.Transition.Phases
{
    using System;
    using VUDK.Patterns.StateMachine;
    using VUDK.Generic.Managers.Main;
    using ProjectM.Features.ExplorationSystem.Transition.Phases.Keys;
    using ProjectM.Constants;

    public class TransitionBegin : TransitionPhaseBase
    {
        public TransitionBegin(Enum stateKey, StateMachine relatedStateMachine, StateMachineContext context) : base(stateKey,  relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
            MainManager.Ins.EventManager.TriggerEvent(GameEventKeys.OnBeginTransition);
            Context.Transition.Begin();
            Context.PreviousNode.OnNodeExit();
            ChangeState(TransitionStateKey.Process);
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
    }
}
