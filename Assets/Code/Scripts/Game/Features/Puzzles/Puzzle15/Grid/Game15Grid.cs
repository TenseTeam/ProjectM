namespace ProjectM.Features.Puzzles.Puzzle15.Grid
{
    using System.Collections.Generic;
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Generic.Structures.Grid;
    using VUDK.Patterns.Initialization.Interfaces;
    using ProjectM.Features.Puzzles.Puzzle15.Grid.Pieces;
    using ProjectM.Features.Puzzles.Puzzle15.Grid.Tiles;
    using ProjectM.Patterns.Factories;

    public class Game15Grid : LayoutGrid<Game15Tile>, IInit<Texture2D>
    {
        private Texture2D _textureSpriteSheet;
        private Dictionary<int, Sprite> _pieceSprites;
        private int _tileIndex;

        public void Init(Texture2D spriteSheet)
        {
            _textureSpriteSheet = spriteSheet;
            _pieceSprites = TextureExtension.CreateSpriteSheet(_textureSpriteSheet, Size.x, Size.y);
            GenerateGrid();
            FillGrid();
        }

        protected override void InitTile(Game15Tile tile, Vector2Int gridPosition)
        {
            base.InitTile(tile, gridPosition);
            tile.Init(_tileIndex++, gridPosition);
        }

        public override void FillGrid()
        {
            Dictionary<int, Sprite> spritesPool = new Dictionary<int, Sprite>(_pieceSprites);
            int lastPieceIndex = _pieceSprites.Count - 1;

            GameObject go = new GameObject("LastPiece");
            SpriteRenderer im = go.AddComponent<SpriteRenderer>();
            im.sprite = spritesPool[lastPieceIndex];

            spritesPool.Remove(lastPieceIndex);

            for (int r = 0; r < Size.x; r++)
            {
                for (int c = 0; c < Size.y; c++)
                {
                    if (r == Size.x - 1 && c == Size.y - 1) break; // Skip last piece

                    var randomElement = spritesPool.GetRandomElementAndRemove();
                    Game15Piece piece = GameFactory.Create(randomElement.Key, randomElement.Value);
                    GridTiles[r, c].InsertPiece(piece);
                }
            }
        }
    }
}