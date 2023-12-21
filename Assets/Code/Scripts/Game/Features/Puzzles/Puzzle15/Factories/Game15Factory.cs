namespace ProjectM.Features.Puzzles.Puzzle15.Factories
{
    using UnityEngine;
    using ProjectM.Features.Puzzles.Grid.Pieces;
    using ProjectM.Features.Puzzles.Machine;
    using ProjectM.Features.Puzzles.Machine.States;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Pooling.Keys;
    using VUDK.Patterns.StateMachine;

    public static class Game15Factory
    {
        /// <summary>
        /// Creates a new instance of Game15Piece using the specified piece index and sprite.
        /// </summary>
        /// <param name="pieceIndex">The index of the puzzle piece.</param>
        /// <param name="sprite">The sprite associated with the puzzle piece.</param>
        /// <returns>A new Game15Piece instance or null if creation fails.</returns>
        public static Game15Piece Create(int pieceIndex, Sprite sprite)
        {
            GameObject go = MainManager.Ins.PoolsManager.Pools[PoolKeys.Game15Piece].Get();

            if (go.TryGetComponent(out Game15Piece piece))
            {
                piece.Init(pieceIndex, sprite);
                return piece;
            }

            return null;
        }

        /// <summary>
        /// Creates a new instance of Game15MachineContext for the specified Game15Puzzle.
        /// </summary>
        /// <param name="puzzle">The Game15Puzzle to associate with the context.</param>
        /// <returns>A new Game15MachineContext instance.</returns>
        public static Game15MachineContext Create(Game15Puzzle puzzle)
        {
            return new Game15MachineContext(puzzle);
        }

        /// <summary>
        /// Creates a new instance of State&lt;Game15MachineContext&gt; based on the specified phase key, related state machine, and context.
        /// </summary>
        /// <param name="phaseKey">The phase key indicating the type of state to create.</param>
        /// <param name="relatedMachine">The state machine related to the new state.</param>
        /// <param name="context">The context associated with the state.</param>
        /// <returns>A new State&lt;Game15MachineContext&gt; instance based on the phase key.</returns>
        public static State<Game15MachineContext> Create(Game15PhaseKey phaseKey, StateMachine relatedMachine, Game15MachineContext context)
        {
            switch (phaseKey)
            {
                case Game15PhaseKey.MovePhase:
                    return new MovePhase(phaseKey, relatedMachine, context);

                case Game15PhaseKey.CheckPhase:
                    return new CheckPhase(phaseKey, relatedMachine, context);
            }

            return null;
        }
    }
}