namespace ProjectM.Assets.Code.Scripts.VUDK.Editor.Tools.Utility
{
    using UnityEditor;
    using UnityEngine;

    public class TexturePlaneRenderer : EditorWindow
    {
        private Texture2D _texture;
        private MeshRenderer _planeMesh;
        private MaterialPropertyBlock _materialPropertyBlock;

        [MenuItem("VUDK/Utility/Texture Plane Renderer")]
        public static void OpenWindow()
        {
            GetWindow<TexturePlaneRenderer>("Texture Plane Renderer");
        }

        private void OnGUI()
        {
            GUILayout.Label("Texture Plane Settings", EditorStyles.boldLabel);

            _texture = (Texture2D)EditorGUILayout.ObjectField("Texture", _texture, typeof(Texture2D), false);
            _planeMesh = (MeshRenderer)EditorGUILayout.ObjectField("Plane Mesh", _planeMesh, typeof(MeshRenderer), true);
            
            GUI.enabled = Check();
            if(GUILayout.Button("Apply Texture"))
                ApplyTextureToPlane();

            GUI.enabled = true;
            EditorGUILayout.Space();
            EditorGUILayout.HelpBox("Tool to seamlessly apply a texture image onto a mesh plane while maintaining its aspect ratio. This utility ensures that the texture is displayed proportionally on the plane, offering a convenient solution for texture mapping.", MessageType.Info);
        }

        private void ApplyTextureToPlane()
        {
            _materialPropertyBlock ??= new MaterialPropertyBlock();
            _planeMesh.GetPropertyBlock(_materialPropertyBlock);

            _materialPropertyBlock.SetTexture("_BaseMap", _texture); // TODO: Check if it is URP or Built-in
            _materialPropertyBlock.SetVector("_BaseMap_ST", new Vector4(1f, 1f, 0f, 0f));
            _planeMesh.SetPropertyBlock(_materialPropertyBlock);

            float aspectRatio = (float)_texture.height / _texture.width;
            _planeMesh.transform.localScale = new Vector3(1f, 1f, aspectRatio);
        }

        private bool Check()
        {
            return _texture && _planeMesh;
        }
    }
}