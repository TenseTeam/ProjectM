namespace ProjectM.Patterns.Factories
{
    using VUDK.Features.Main.Camera.CameraModifiers;
    using VUDK.Features.More.ExplorationSystem.Transition.Types.Keys;
    using VUDK.Features.More.ExplorationSystem.Transition.Types;
    using VUDK.Generic.Serializable;
    using VUDK.Features.More.ExplorationSystem.Transition;
    using ProjectM.Features.Puzzles.Puzzle15.Grid.Pieces;
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Patterns.Pooling.Keys;

    /// <summary>
    /// Factory responsible for creating game-related objects.
    /// </summary>
    public static class GameFactory
    {
        public static Game15Piece Create(Sprite sprite)
        {
            GameObject go = MainManager.Ins.PoolsManager.Pools[PoolKeys.Game15Piece].Get();

            if (go.TryGetComponent(out Game15Piece piece))
            {
                piece.Init(sprite);
                return piece;
            }

            return null;
        }
    }
}