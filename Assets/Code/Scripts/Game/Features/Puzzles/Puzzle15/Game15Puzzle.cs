namespace ProjectM.Features.Puzzles
{
    using System;
    using UnityEngine;
    using ProjectM.Features.Puzzles.Grid;
    using ProjectM.Features.Puzzles.Machine;
    using ProjectM.Features.Puzzles.Grid.Tiles;
    using ProjectM.Features.Puzzles.Machine.States;
    using VUDK.Features.More.PuzzleSystem.Saving;

    [RequireComponent(typeof(Game15Grid))]
    [RequireComponent(typeof(Game15Machine))]
    public class Game15Puzzle : SavedPuzzleBase
    {
        [Header("Puzzle Pieces")]
        [SerializeField]
        private Texture2D _game15Texture;

        protected Game15Grid Grid;
        protected Game15Machine Machine;

        public Game15Tile[,] Tiles => Grid.GridTiles;
        public Game15Tile EmptyTile => Grid.EmptyTile;

        public event Action OnMovedPiece;

        protected override void Awake()
        {
            base.Awake();
            TryGetComponent(out Grid);
            TryGetComponent(out Machine);
        }

        private void Start()
        {
            Init();
        }

        public void Init()
        {
            Machine.Init(this);
            Grid.Init(this, _game15Texture, IsSolved);
        }

        public override void BeginPuzzle()
        {
            base.BeginPuzzle();
            if (IsSolved)
            {
                if (IsRepeatable)
                    Grid.Shuffle();
                else
                    return;
            }

            Machine.StartMachine();
        }

        public void MovePieceInEmptyTile(Game15Tile tile)
        {
            if (!IsFocused) return;
            if (!Machine.IsState(Game15PhaseKey.MovePhase)) return;

            tile.SwitchPieceAdjacently(EmptyTile, Grid, OnMovedPiece);
        }
    }
}