namespace ProjectM.Features.Puzzles
{
    using ProjectM.Features.Puzzles.Grid;
    using ProjectM.Features.Puzzles.Grid.Pieces;
    using ProjectM.Features.Puzzles.Grid.Tiles;
    using System;

    public static class Game15PiecesController
    {
        /// <summary>
        /// Swaps the position of the current tile with an adjacent tile in the 15-puzzle game grid.
        /// </summary>
        /// <param name="fromTile">The tile to move.</param>
        /// <param name="adjacentTile">The adjacent tile to swap positions with.</param>
        /// <param name="grid">The 15-puzzle game grid.</param>
        /// <param name="onSwitchCallback">Optional action to be executed after the swap.</param>
        public static void SwitchPieceAdjacently(this Game15Tile fromTile, Game15Tile adjacentTile, Game15Grid grid, Action onSwitchCallback = null)
        {
            if (grid.CheckTileAdjacency(fromTile.GridPosition, adjacentTile.GridPosition))
            {
                fromTile.SwitchPiece(adjacentTile, grid);
                onSwitchCallback?.Invoke();
            }
        }

        /// <summary>
        /// Swaps the position of the current tile with a target tile in the 15-puzzle game grid.
        /// </summary>
        /// <param name="fromTile">The tile to start the swap from.</param>
        /// <param name="targetTile">The target tile to swap positions with.</param>
        /// <param name="grid">The 15-puzzle game grid.</param>
        public static void SwitchPiece(this Game15Tile fromTile, Game15Tile targetTile, Game15Grid grid)
        {
            Game15Piece tempPiece = fromTile.GetRemovePiece();

            if (targetTile.IsOccupied)
                fromTile.InsertPiece(targetTile.InsertedPiece);

            targetTile.InsertPiece(tempPiece);
        }

    }
}