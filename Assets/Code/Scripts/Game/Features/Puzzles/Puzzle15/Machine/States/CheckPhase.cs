namespace ProjectM.Features.Puzzles.Machine.States
{
    using ProjectM.Features.Puzzles.Grid.Tiles;
    using System;
    using VUDK.Patterns.StateMachine;
    using UnityEngine;

    public class CheckPhase : State<Game15MachineContext>
    {
        public CheckPhase(Enum stateKey, StateMachine relatedStateMachine, StateContext context) : base(stateKey, relatedStateMachine, context)
        {
        }

        public override void Enter()
        {
            if (CheckPiecesOrder())
                Context.Puzzle.ResolvePuzzle();
            else
                ChangeState(Game15PhaseKey.MovePhase);
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

        private bool CheckPiecesOrder()
        {
            Game15Tile[,] tiles = Context.Puzzle.Tiles;

            for(int c = 0; c < tiles.GetLength(0); c++)
            {
                for(int r = 0; r < tiles.GetLength(1); r++)
                {
                    if(c == tiles.GetLength(0) - 1 && r == tiles.GetLength(1) - 1)
                        break;

                    Game15Tile tile = tiles[c, r];
                    if (!tile.IsOccupied)
                        return false;

                    if (tile.TileIndex != tile.InsertedPiece.PieceIndex)
                        return false;
                }
            }

            return true;
        }
    }
}