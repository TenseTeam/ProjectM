namespace ProjectM.Features.ExplorationSystem.Transition.Phases
{
    using System;
    using UnityEngine;
    using VUDK.Patterns.StateMachine;
    using ProjectM.Features.ExplorationSystem.Transition.Phases.Keys;

    public class TransitionProcess : TransitionPhaseBase
    {
        public TransitionProcess(Enum stateKey, StateMachine relatedStateMachine, StateMachineContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
            Context.Transition.OnTransitionCompleted += OnTransitionComplete;
        }

        public override void Process()
        {
            Context.Transition.Process();
        }

        public override void FixedProcess()
        {
        }

        public override void Exit()
        {
            Context.Transition.OnTransitionCompleted -= OnTransitionComplete;
        }

        private void OnTransitionComplete()
        {
            ChangeState(TransitionStateKey.End);
        }
    }
}
