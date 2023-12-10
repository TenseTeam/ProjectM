namespace ProjectM.Features.Puzzles.Puzzle15.Grid.Tiles
{
    using UnityEngine;
    using VUDK.Generic.Structures.Grid.Bases;
    using ProjectM.Features.Puzzles.Puzzle15.Grid.Pieces;
    using VUDK.Extensions;

    public class Game15Tile : GridTileBase
    {
        public Game15Piece InsertedPiece { get; private set; }
        public int TileIndex { get; private set; }

        public bool IsOccupied => InsertedPiece != null;

        public void Init(int tileIndex, Vector2Int position)
        {
            base.Init(position);
            TileIndex = tileIndex;
        }

        public void InsertPiece(Game15Piece piece)
        {
            InsertedPiece = piece;
            piece.transform.SetParent(transform, false);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            DrawBorders();
            DrawOccupancy();
        }

        private void DrawOccupancy()
        {
            Gizmos.color = IsOccupied ? new Color(1, 0, 0, .5f) : new Color(0, 1, 0, .5f);
            GizmosExtension.DrawCube(transform.position, (Vector2)transform.localScale, transform.rotation);
        }

        private void DrawBorders() 
        {
            Gizmos.color = Color.black;
            GizmosExtension.DrawWireCube(transform.position, (Vector2)transform.localScale, transform.rotation);
        }
#endif
    }
}