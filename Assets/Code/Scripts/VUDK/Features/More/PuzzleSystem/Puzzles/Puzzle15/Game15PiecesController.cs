namespace VUDK.Features.More.PuzzleSystem.Puzzles
{
    using VUDK.Features.More.PuzzleSystem.Puzzles.Grid;
    using VUDK.Features.More.PuzzleSystem.Puzzles.Grid.Pieces;
    using VUDK.Features.More.PuzzleSystem.Puzzles.Grid.Tiles;
    using System;

    public static class Game15PiecesController
    {
        public static void SwitchPieceAdjacently(this Game15Tile fromTile, Game15Tile adjacentTile, Game15Grid grid, Action onSwitchCallback = null)
        {
            if (grid.CheckTileAdjacency(fromTile.GridPosition, adjacentTile.GridPosition))
            {
                fromTile.SwitchPiece(adjacentTile, grid);
                onSwitchCallback?.Invoke();
            }
        }

        public static void SwitchPiece(this Game15Tile fromTile, Game15Tile targetTile, Game15Grid grid)
        {
            Game15Piece tempPiece = fromTile.GetRemovePiece();
            if(targetTile.IsOccupied)
                fromTile.InsertPiece(targetTile.InsertedPiece);

            targetTile.InsertPiece(tempPiece);
        }
    }
}