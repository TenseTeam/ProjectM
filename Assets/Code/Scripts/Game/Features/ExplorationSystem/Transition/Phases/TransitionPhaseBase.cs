namespace ProjectM.Features.ExplorationSystem.Transition.Phases
{
    using System;
    using VUDK.Patterns.StateMachine;

    public abstract class TransitionPhaseBase : State<TransitionContext>
    {
        public TransitionPhaseBase(Enum stateKey, StateMachine relatedStateMachine, StateMachineContext context) : base(stateKey, relatedStateMachine, context)
        {
        }
    }
}
