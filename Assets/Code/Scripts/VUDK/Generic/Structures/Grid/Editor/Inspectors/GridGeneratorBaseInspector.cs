namespace VUDK.Generic.Structures.Grid.Editor.Inspectors
{
    using UnityEditor;
    using UnityEngine;
    using VUDK.Generic.Structures.Grid.Bases;

    [CustomEditor(typeof(GridGeneratorBase), true)]
    public class GridGeneratorBaseInspector : Editor
    {
        private GridGeneratorBase _gridGenerator;

        private void OnEnable()
        {
            _gridGenerator = target as GridGeneratorBase;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if(GUILayout.Button("Generate Grid"))
                _gridGenerator.GenerateGrid();
            if(GUILayout.Button("Clear Grid"))
                _gridGenerator.ClearGrid();
            GUILayout.Space(4);
        }
    }
}
