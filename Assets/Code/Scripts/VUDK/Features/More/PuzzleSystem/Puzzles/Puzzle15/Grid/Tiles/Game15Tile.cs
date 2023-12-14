namespace VUDK.Features.More.PuzzleSystem.Puzzles.Grid.Tiles
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Extensions;
    using VUDK.Generic.Structures.Grid.Bases;
    using VUDK.Features.More.PuzzleSystem.Puzzles.Grid.Pieces;
    using System;

    [RequireComponent(typeof(Button))]
    public class Game15Tile : GridTileBase
    {
        private Button _button;
        private Game15Puzzle _puzzle;

        public Game15Piece InsertedPiece { get; private set; }
        public int TileIndex { get; private set; }

        public bool IsOccupied => InsertedPiece != null;

        private void Awake()
        {
            TryGetComponent(out _button);
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(MovePiece);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(MovePiece);
        }

        public void Init(Game15Puzzle puzzle, int tileIndex, Vector2Int position)
        {
            base.Init(position);
            _puzzle = puzzle;
            TileIndex = tileIndex;
        }

        public void InsertPiece(Game15Piece piece)
        {
            InsertedPiece = piece;
            piece.transform.SetParent(transform, false);
        }

        public void RemovePiece()
        {
            InsertedPiece.Dispose();
            InsertedPiece = null;
        }

        public Game15Piece GetRemovePiece()
        {
            Game15Piece game15Piece = InsertedPiece;
            InsertedPiece = null;
            return game15Piece;
        }

        private void MovePiece()
        {
            _puzzle.MovePieceInEmptyTile(this);
        }
    }
}