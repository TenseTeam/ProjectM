namespace ProjectM.Features.Puzzles.Machine.States
{
    using System;
    using VUDK.Patterns.StateMachine;

    public class MovePhase : State<Game15MachineContext>
    {
        public MovePhase(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        /// <inheritdoc/>
        public override void Enter()
        {
            Context.Puzzle.OnMovedPiece += OnMovedPiece;
        }

        /// <inheritdoc/>
        public override void Exit()
        {
            Context.Puzzle.OnMovedPiece -= OnMovedPiece;
        }

        /// <inheritdoc/>
        public override void FixedProcess()
        {
        }

        /// <inheritdoc/>
        public override void Process()
        {
        }

        /// <inheritdoc/>
        private void OnMovedPiece()
        {
            ChangeState(Game15PhaseKey.CheckPhase);
        }
    }
}