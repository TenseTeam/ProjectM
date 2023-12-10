namespace ProjectM.Features.Puzzles.Puzzle15.Grid.Pieces
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Patterns.Initialization.Interfaces;
    using VUDK.Patterns.Pooling;

    [RequireComponent(typeof(Image))]
    public class Game15Piece : PooledObject, IInit<Sprite>
    {
        private Image _image;

        private void Awake()
        {
            TryGetComponent(out _image);
        }

        public void Init(Sprite sprite)
        {
            _image.sprite = sprite;
        }

        public bool Check()
        {
            return _image != null;
        }
    }
}