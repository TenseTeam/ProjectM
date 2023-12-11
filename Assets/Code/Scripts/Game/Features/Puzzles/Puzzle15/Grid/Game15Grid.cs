﻿namespace ProjectM.Features.Puzzles.Puzzle15.Grid
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Generic.Structures.Grid;
    using ProjectM.Features.Puzzles.Puzzle15.Grid.Pieces;
    using ProjectM.Features.Puzzles.Puzzle15.Grid.Tiles;
    using ProjectM.Patterns.Factories;

    public class Game15Grid : LayoutGrid<Game15Tile>
    {
        private Texture2D _textureSpriteSheet;
        private Dictionary<int, Sprite> _piecesSprites;
        private Game15Puzzle _puzzle;
        private int _tileIndex;

        private void OnValidate()
        {
            RectTransform.sizeDelta = new Vector2(Size.x, Size.y);
        }

        public void Init(Game15Puzzle puzzle, Texture2D spriteSheet)
        {
            _textureSpriteSheet = spriteSheet;
            _puzzle = puzzle;
            _piecesSprites = TextureExtension.CreateSpriteSheet(_textureSpriteSheet, Size.x, Size.y);
            GenerateGrid();
            Shuffle();
        }

        protected override void InitTile(Game15Tile tile, Vector2Int gridPosition)
        {
            base.InitTile(tile, gridPosition);
            tile.Init(_puzzle, _tileIndex++, gridPosition);
        }

        public override void FillGrid()
        {
            ClearPieces();

            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    if (x == Size.x - 1 && y == Size.y - 1) break; // Skip last piece

                    int index = x + y * Size.x;
                    var element = _piecesSprites.ElementAt(index);
                    Game15Piece piece = GameFactory.Create(element.Key, element.Value);
                    GridTiles[x, y].InsertPiece(piece);
                }
            }
        }

        public void ClearPieces()
        {
            foreach (var tile in GridTiles)
            {
                if (tile.IsOccupied)
                  tile.RemovePiece();
            }
        }

        public void Shuffle()
        {
            FillGrid(); // To make sure is in order

            int maxSize = Mathf.Max(Size.x, Size.y);
            int shuffleCount = maxSize % 2 == 0 && (Size.x != Size.y) ? 15 : 30;

            for(int i = 0; i < shuffleCount; i++)
            {
                Game15Tile tileA;
                Game15Tile tileB;

                do
                {
                    tileA = GetRandomTile();
                    tileB = GetRandomTile();
                } while (tileA.TileIndex == tileB.TileIndex);

                tileA.SwitchPiece(tileB, this);
            }
        }

        public Game15Tile GetEmptyTile()
        {
            foreach (var tile in GridTiles)
            {
                if (!tile.IsOccupied) return tile;
            }

            return null;
        }

        private Game15Tile GetRandomTile()
        {
            int x, y;

            do
            {
                x = Random.Range(0, Size.x);
                y = Random.Range(0, Size.y);
            } while ( (x == Size.x - 1 && y == Size.y - 1)); // Do not touch the empty tile

            return GridTiles[x, y];
        }
    }
}