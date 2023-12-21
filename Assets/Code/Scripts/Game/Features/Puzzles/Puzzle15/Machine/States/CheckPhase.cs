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

        /// <inheritdoc/>
        public override void Enter()
        {
            if (CheckPiecesOrder())
                Context.Puzzle.ResolveTask();
            else
                ChangeState(Game15PhaseKey.MovePhase);
        }

        /// <inheritdoc/>
        public override void Exit()
        {
        }

        /// <inheritdoc/>
        public override void FixedProcess()
        {
        }

        /// <inheritdoc/>
        public override void Process()
        {
        }

        /// <summary>
        /// Checks the pieces order.
        /// </summary>
        /// <returns>True if pieces are in correct order, False if not.</returns>
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