namespace ProjectM.Features.Artwork.Data
{
    using UnityEngine;

    [CreateAssetMenu(fileName = nameof(ArtworkInfoData), menuName = "Project/Artwork")]
    public sealed class ArtworkInfoData : ScriptableObject
    {
        public string Name;
        [TextArea(3, 10)]
        public string Description;
    }
}
