namespace VUDK.Features.Packages.ExplorationSystem.Transition.Phases
{
    using System;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.Packages.ExplorationSystem.Transition.Phases.Keys;
    using VUDK.Features.Packages.ExplorationSystem.Constants;
    using VUDK.Features.Main.EventSystem;

    public class TransitionBegin : TransitionPhaseBase
    {
        public TransitionBegin(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey,  relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
            EventManager.Ins.TriggerEvent(ExplorationEventKeys.OnBeginTransition);
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
