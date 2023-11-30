namespace ProjectM.Features.Artwork.Data
{
    using UnityEngine;

    [CreateAssetMenu(fileName = "ArtworkData", menuName = "GameData/Artwork", order = 1)]
    public class ArtworkData : ScriptableObject
    {
        public string Name;
        [TextArea(3, 10)]
        public string Description;
    }
}
