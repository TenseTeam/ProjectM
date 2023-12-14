namespace VUDK.Features.More.PuzzleSystem.Puzzles.Grid.Pieces
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Extensions;
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
            transform.name = $"Game15Piece {pieceIndex}";
            _image.sprite = sprite;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            DrawPieceIndex();
        }

        private void DrawPieceIndex()
        {
            GUIStyle style = new GUIStyle()
            {
                wordWrap = true,
                alignment = TextAnchor.MiddleCenter,
                fontSize = 20,
                fontStyle = FontStyle.Bold,
            };
            UnityEditor.Handles.Label(transform.position, PieceIndex.ToString(), style);
        }
#endif
    }
}