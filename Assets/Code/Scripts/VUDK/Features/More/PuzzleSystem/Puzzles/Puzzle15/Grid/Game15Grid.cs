namespace VUDK.Features.More.PuzzleSystem.Puzzles.Grid
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Generic.Structures.Grid;
    using VUDK.Features.More.PuzzleSystem.Puzzles.Grid.Pieces;
    using VUDK.Features.More.PuzzleSystem.Puzzles.Grid.Tiles;
    using VUDK.Features.More.PuzzleSystem.Puzzles.Puzzle15.Factories;

    public class Game15Grid : LayoutGrid<Game15Tile>
    {
        private const int InversionsCount = 20;

        private Texture2D _textureSpriteSheet;
        private Dictionary<int, Sprite> _piecesSprites;
        private Game15Puzzle _puzzle;
        private int _tileIndex;

        public Game15Tile EmptyTile
        {
            get
            {
                foreach (var tile in GridTiles)
                {
                    if (!tile.IsOccupied) return tile;
                }

                return null;
            }
        }

        public void Init(Game15Puzzle puzzle, Texture2D spriteSheet, bool isSolved)
        {
            puzzle.OnPuzzleResolved.AddListener(FillLastTile);
            _textureSpriteSheet = spriteSheet;
            _puzzle = puzzle;
            _piecesSprites = TextureExtension.CreateSpriteSheet(_textureSpriteSheet, Size.x, Size.y);
            GenerateGrid();

            if (!isSolved)
            {
                Shuffle();
            }
            else
            {
                FillGrid();
                FillLastTile();
            }
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
                    Game15Piece piece = Game15Factory.Create(element.Key, element.Value);
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

            for(int i = 0; i < InversionsCount; i++)
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

            if (!IsSolvable())
                GridTiles[0, 0].SwitchPiece(GridTiles[GridTiles.GetLength(0) - 1, GridTiles.GetLength(1) - 1], this);
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

        private bool IsSolvable()
        {   
            bool isSolvable;
            bool isPairOrSquareGrid = Size.x == Size.y || (Size.x * Size.y) % 2 == 0;

            if (isPairOrSquareGrid)
                isSolvable = InversionsCount % 2 == 0;
            else
                isSolvable = InversionsCount % 2 != 0 && EmptyTile.GridPosition.y % 2 != 0;

            return isSolvable;
        }

        private void FillLastTile()
        {
            GridTiles[Size.x - 1, Size.y - 1].InsertPiece(Game15Factory.Create(0, _piecesSprites[_piecesSprites.Count - 1]));
        }
    }
}