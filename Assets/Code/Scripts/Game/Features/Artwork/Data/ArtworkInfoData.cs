namespace ProjectM.Features.Artwork.Data
{
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(ArtworkInfoData), menuName = "GameData/Artwork Info", order = 1)]
    public sealed class ArtworkInfoData : ScriptableObject
    {
        public string Name;
        [TextArea(3, 10)]
        public string Description;
    }
}
