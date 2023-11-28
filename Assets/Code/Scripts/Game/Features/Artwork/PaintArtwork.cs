namespace ProjectM.Features.Artwork
{
    using UnityEngine;
    using ProjectM.Features.Artwork.Data;

    public class PaintArtwork : Artwork
    {
        [Header("Paint Plane Mesh")]
        [SerializeField]
        private MeshRenderer _paintPlane;

        public new PaintArtworkData ArtworkData => base.ArtworkData as PaintArtworkData;

        public override void Init()
        {
            base.Init();
            SetImageOnPlane();
        }

        private void SetImageOnPlane()
        {
            _paintPlane.material.mainTexture = ArtworkData.PaintTexture;
            float aspectRatio = (float)ArtworkData.PaintTexture.height / ArtworkData.PaintTexture.width;
            _paintPlane.transform.localScale = new Vector3(1f, 1f, aspectRatio); // On Z axis because is a plane
        }
    }
}