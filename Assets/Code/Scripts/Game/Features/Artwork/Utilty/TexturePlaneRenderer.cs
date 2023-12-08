namespace ProjectM.Features.Artwork.Utilty
{
    using UnityEngine;

    public class TexturePlaneRenderer : MonoBehaviour
    {
        [Header("Texture Settings")]
        [SerializeField]
        private Texture2D _texture;
        [SerializeField]
        private MeshRenderer _plane;

        private MaterialPropertyBlock _materialPropertyBlock;

        private void OnValidate()
        {
            if (_texture != null)
                SetImageOnPlane();
        }

        private void SetImageOnPlane()
        {
            _materialPropertyBlock ??= new MaterialPropertyBlock();
            _plane.GetPropertyBlock(_materialPropertyBlock);
            _materialPropertyBlock.SetTexture("_BaseMap", _texture);
            _materialPropertyBlock.SetVector("_BaseMap_ST", new Vector4(1f, 1f, 0f, 0f));
            _plane.SetPropertyBlock(_materialPropertyBlock);

            ScalePlaneByTextureResolution(_texture);
        }

        private void ScalePlaneByTextureResolution(Texture2D texture)
        {
            float aspectRatio = (float)texture.height / texture.width;
            _plane.transform.localScale = new Vector3(1f, 1f, aspectRatio); // On Z axis because is a plane
        }
    }
}