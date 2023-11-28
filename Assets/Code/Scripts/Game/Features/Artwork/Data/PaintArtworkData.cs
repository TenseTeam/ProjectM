namespace ProjectM.Features.Artwork.Data
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "PaintArtworkData", menuName = "GameData/Paint", order = 1)]
    public class PaintArtworkData : ArtworkData
    {
        public Texture2D PaintTexture;
    }
}
