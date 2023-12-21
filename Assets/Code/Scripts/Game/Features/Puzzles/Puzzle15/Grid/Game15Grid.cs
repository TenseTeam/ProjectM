namespace ProjectM.Features.Puzzles.Grid
{
    using System.Collections.Generic;
    using System.Linq;
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Generic.Structures.Grid;
    using ProjectM.Features.Puzzles.Grid.Pieces;
    using ProjectM.Features.Puzzles.Grid.Tiles;
    using ProjectM.Features.Puzzles.Puzzle15.Factories;

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

        /// <summary>
        /// Initializes the Game15Puzzle object with the specified parameters.
        /// </summary>
        /// <param name="puzzle">The Game15Puzzle to initialize.</param>
        /// <param name="spriteSheet">The sprite sheet containing puzzle piece textures.</param>
        /// <param name="isSolved">Flag indicating whether the puzzle should be initialized as solved.</param>
        public void Init(Game15Puzzle puzzle, Texture2D spriteSheet, bool isSolved)
        {
            puzzle.OnTaskResolved.AddListener(FillLastTile);
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

        /// <summary>
        /// Initializes a Game15Tile with the specified grid position, assigning puzzle-related properties.
        /// </summary>
        /// <param name="tile">The Game15Tile to initialize.</param>
        /// <param name="gridPosition">The grid position of the tile.</param>
        protected override void InitTile(Game15Tile tile, Vector2Int gridPosition)
        {
            base.InitTile(tile, gridPosition);
            tile.Init(_puzzle, _tileIndex++, gridPosition);
        }

        /// <summary>
        /// Fills the grid with puzzle pieces.
        /// </summary>
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

        /// <summary>
        /// Clears the grid of puzzle pieces.
        /// </summary>
        public void ClearPieces()
        {
            foreach (var tile in GridTiles)
            {
                if (tile.IsOccupied)
                  tile.RemovePiece();
            }
        }

        /// <summary>
        /// Shuffles the puzzle pieces.
        /// </summary>
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

        /// <summary>
        /// Gets a random tile from the grid.
        /// </summary>
        /// <returns>A random tile from the grid.</returns>
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

        /// <summary>
        /// Checks if the puzzle is solvable.
        /// </summary>
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

        /// <summary>
        /// Fills the last tile with the last puzzle piece.
        /// </summary>
        private void FillLastTile()
        {
            GridTiles[Size.x - 1, Size.y - 1].InsertPiece(Game15Factory.Create(_piecesSprites.Count - 1, _piecesSprites[_piecesSprites.Count - 1]));
        }
    }
}