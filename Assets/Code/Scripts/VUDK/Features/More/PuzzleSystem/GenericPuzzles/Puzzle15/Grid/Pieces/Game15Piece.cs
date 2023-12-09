namespace VUDK.Features.More.PuzzleSystem.GenericPuzzles.Puzzle15.Grid.Pieces
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Patterns.Initialization.Interfaces;

    [RequireComponent(typeof(Image))]
    public class Game15Piece : MonoBehaviour, IInit<Sprite>
    {
        public Image _image;

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