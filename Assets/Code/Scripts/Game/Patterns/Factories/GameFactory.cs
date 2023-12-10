namespace ProjectM.Patterns.Factories
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Pooling.Keys;
    using ProjectM.Features.Puzzles.Puzzle15.Grid.Pieces;

    /// <summary>
    /// Factory responsible for creating game-related objects.
    /// </summary>
    public static class GameFactory
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
    }
}