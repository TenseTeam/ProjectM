namespace ProjectM.Features.Artwork
{
    using ProjectM.Features.Artwork.Data;
    using UnityEngine;

    public class PaintArtwork : Artwork
    {
        [Header("Paint Plane Mesh")]
        [SerializeField]
        private MeshRenderer _paintPlane;

        private MaterialPropertyBlock _materialPropertyBlock;

        public new PaintArtworkData ArtworkData => base.ArtworkData as PaintArtworkData;

        public override void Init()
        {
            base.Init();
            SetImageOnPlane();
        }

        public override bool Check()
        {
            return base.Check() && _paintPlane != null;
        }

        private void SetImageOnPlane()
        {
            _materialPropertyBlock ??= new MaterialPropertyBlock();
            _paintPlane.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetTexture("_BaseMap", ArtworkData.PaintTexture);
            _materialPropertyBlock.SetVector("_BaseMap_ST", new Vector4(1f, 1f, 0f, 0f));
            _paintPlane.SetPropertyBlock(_materialPropertyBlock);

            ScalePlaneByTextureResolution(ArtworkData.PaintTexture);
        }

        private void ScalePlaneByTextureResolution(Texture2D texture)
        {
            float aspectRatio = (float)texture.height / texture.width;
            _paintPlane.transform.localScale = new Vector3(1f, 1f, aspectRatio); // On Z axis because is a plane
        }
    }
}