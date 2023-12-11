namespace ProjectM.Features.Puzzles.Puzzle15.Grid.Pieces
{
    using System;
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Patterns.Pooling;

    [RequireComponent(typeof(Image))]
    public class Game15Piece : PooledObject
    {
        private Image _image;

        public int PieceIndex { get; private set; }

        private void Awake()
        {
            TryGetComponent(out _image);

        }

        public void Init(int pieceIndex, Sprite sprite)
        {
            PieceIndex = pieceIndex;
            _image.sprite = sprite;
        }
    }
}