namespace VUDK.Features.Packages.ExplorationSystem.Transition.Phases
{
    using System;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.Packages.ExplorationSystem.Constants;

    public class TransitionEnd : TransitionPhaseBase
    {
        public TransitionEnd(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
            EventManager.Ins.TriggerEvent(ExplorationEventKeys.OnEndTransition);
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
    }
}
