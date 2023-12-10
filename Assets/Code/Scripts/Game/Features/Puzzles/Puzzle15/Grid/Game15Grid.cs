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
        private List<Sprite> _sprites;

        private int _lastPieceIndex;

        public void Init(Texture2D spriteSheet)
        {
            base.Init();
            _textureSpriteSheet = spriteSheet;
            _sprites = TextureExtension.CreateSpriteSheet(_textureSpriteSheet, Size.x, Size.y);
            _lastPieceIndex = _sprites.Count - Size.y;
            FillGrid();
        }

        public override void FillGrid()
        {
            List<Sprite> spritesPool = new List<Sprite>(_sprites);
            spritesPool.RemoveAt(_lastPieceIndex);

            for (int r = 0; r < Size.x; r++)
            {
                for (int c = 0; c < Size.y; c++)
                {
                    if (r == Size.x - 1 && c == Size.y - 1) break; // Skip last piece

                    int randomIndex = Random.Range(0, spritesPool.Count);
                    Sprite pickedSprite = spritesPool[randomIndex];
                    Game15Piece piece = GameFactory.Create(pickedSprite);
                    GridTiles[r, c].InsertPiece(piece);
                    spritesPool.Remove(pickedSprite);
                }
            }
        }
    }
}