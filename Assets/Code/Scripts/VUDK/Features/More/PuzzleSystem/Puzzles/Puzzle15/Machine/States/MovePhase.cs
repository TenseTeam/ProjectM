namespace VUDK.Features.More.PuzzleSystem.Puzzles.Machine.States
{
    using System;
    using VUDK.Patterns.StateMachine;

    public class MovePhase : State<Game15MachineContext>
    {
        public MovePhase(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
            Context.Puzzle.OnMovedPiece += OnMovedPiece;
        }

        public override void Exit()
        {
            Context.Puzzle.OnMovedPiece -= OnMovedPiece;
        }

        public override void FixedProcess()
        {
        }

        public override void Process()
        {
        }

        private void OnMovedPiece()
        {
            ChangeState(Game15PhaseKey.CheckPhase);
        }

    }
}