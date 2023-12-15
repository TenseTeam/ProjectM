namespace VUDK.Features.More.PuzzleSystem.Puzzles
{
    using System;
    using UnityEngine;
    using VUDK.Features.More.PuzzleSystem.Bases;
    using VUDK.Features.More.PuzzleSystem.Puzzles.Grid;
    using VUDK.Features.More.PuzzleSystem.Puzzles.Grid.Tiles;
    using VUDK.Features.More.PuzzleSystem.Puzzles.Machine;
    using VUDK.Features.More.PuzzleSystem.Puzzles.Machine.States;

    [RequireComponent(typeof(Game15Grid))]
    [RequireComponent(typeof(Game15Machine))]
    public class Game15Puzzle : DatePuzzleBase
    {
        [Header("Puzzle Pieces")]
        [SerializeField]
        private Texture2D _game15Texture;

        private Game15Grid _grid;
        private Game15Machine _machine;

        public Game15Tile[,] Tiles => _grid.GridTiles;

        public Game15Tile EmptyTile => _grid.EmptyTile;

        public event Action OnMovedPiece;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out _grid);
            TryGetComponent(out _machine);
        }

        protected override void Start()
        {
            base.Start();
            _machine.Init(this);
            _grid.Init(this, _game15Texture, IsSolved);
        }

        public override void BeginPuzzle()
        {
            base.BeginPuzzle();
            if (IsSolved)
            {
                if (IsRepeatable)
                    _grid.Shuffle();
                else
                    return;
            }

            _machine.StartMachine();
        }

        public void MovePieceInEmptyTile(Game15Tile tile)
        {
            if (!IsFocused) return;
            if (!_machine.IsState(Game15PhaseKey.MovePhase)) return;

            tile.SwitchPieceAdjacently(EmptyTile, _grid, OnMovedPiece);
        }
    }
}