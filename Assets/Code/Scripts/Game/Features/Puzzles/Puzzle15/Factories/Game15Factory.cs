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

        public static Game15MachineContext Create(Game15Puzzle puzzle)
        {
            return new Game15MachineContext(puzzle);
        }

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