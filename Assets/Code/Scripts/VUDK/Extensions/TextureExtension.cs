namespace VUDK.Extensions
{
    using System.Collections.Generic;
    using UnityEngine;

    public static class TextureExtension
    {
        public static List<Sprite> CreateSpriteSheet(Texture2D textureSpriteSheet, int columns, int rows)
        {
            List<Sprite> sprites = new List<Sprite>();
            int spriteWidth = textureSpriteSheet.width / columns;
            int spriteHeight = textureSpriteSheet.height / rows;

            for (int col = 0; col < columns; col++)
            {
                for (int row = 0; row < rows; row++)
                {
                    Rect rect = new Rect(col * spriteWidth, row * spriteHeight, spriteWidth, spriteHeight);

                    Texture2D spriteTexture = new Texture2D((int)rect.width, (int)rect.height);
                    Color[] colors = textureSpriteSheet.GetPixels((int)rect.x, (int)rect.y, (int)rect.width, (int)rect.height);
                    spriteTexture.SetPixels(colors);
                    spriteTexture.Apply();

                    sprites.Add(Sprite.Create(spriteTexture, new Rect(0, 0, spriteWidth, spriteHeight), new Vector2(0.5f, 0.5f)));
                }
            }

            return sprites;
        }
    }
}