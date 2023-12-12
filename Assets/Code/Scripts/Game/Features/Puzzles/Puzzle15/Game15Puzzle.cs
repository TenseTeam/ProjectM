namespace ProjectM.Features.Puzzles.Puzzle15
{
    using System;
    using UnityEngine;
    using VUDK.Features.More.PuzzleSystem.Bases;
    using ProjectM.Features.Puzzles.Puzzle15.Grid;
    using ProjectM.Features.Puzzles.Puzzle15.Grid.Tiles;
    using ProjectM.Features.Puzzles.Puzzle15.Machine;
    using ProjectM.Features.Puzzles.Puzzle15.Machine.States;

    [RequireComponent(typeof(Game15Grid))]
    [RequireComponent(typeof(Game15Machine))]
    public class Game15Puzzle : PuzzleBase
    {
        [Header("Puzzle Pieces")]
        [SerializeField]
        private Texture2D _game15Texture;

        private Game15Grid _grid;
        private Game15Machine _machine;

        public Game15Tile[,] Tiles => _grid.GridTiles;

        public Game15Tile EmptyTile => _grid.EmptyTile;

        public event Action OnMovedPiece;

        private void Awake()
        {
            TryGetComponent(out _grid);
            TryGetComponent(out _machine);
        }

        private void Start()
        {
            _machine.Init(this);
            _grid.Init(this, _game15Texture);
        }

        public override void BeginPuzzle()
        {
            ResumePuzzle();

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
            if (!IsInProgress) return;
            if (!_machine.IsState(Game15PhaseKey.MovePhase)) return;

            tile.SwitchPieceAdjacently(EmptyTile, _grid, OnMovedPiece);
        }
    }
}