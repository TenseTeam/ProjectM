namespace ProjectM.Features.Puzzles.Puzzle15
{
    using UnityEngine;
    using VUDK.Features.More.PuzzleSystem.Bases;
    using ProjectM.Features.Puzzles.Puzzle15.Grid;
    using ProjectM.Features.Puzzles.Puzzle15.Machine;

    [RequireComponent(typeof(Game15Grid))]
    [RequireComponent(typeof(Game15Machine))]
    public class Game15Puzzle : PuzzleBase
    {
        [Header("Puzzle Pieces")]
        [SerializeField]
        private Texture2D _game15Texture;

        private Game15Grid _grid;
        private Game15Machine _machine;

        private void Awake()
        {
            TryGetComponent(out _grid);
            TryGetComponent(out _machine);
        }

        private void Start()
        {
            _machine.Init(this);
            _grid.Init(_game15Texture);
        }

        public override void BeginPuzzle()
        {
            base.BeginPuzzle();
            _machine.StartMachine();
        }
    }
}