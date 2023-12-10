namespace ProjectM.Features.Puzzles.Puzzle15.Machine.States
{
    using System;
    using VUDK.Patterns.StateMachine;

    public class CheckPhase : State<Game15MachineContext>
    {
        public CheckPhase(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
        }

        public override void Exit()
        {
        }

        public override void FixedProcess()
        {
        }

        public override void Process()
        {
        }
    }
}