namespace Assets.Code.Scripts.Game.Features.ExplorationSystem.Editor
{
    using UnityEditor;
    using UnityEngine;
    using ProjectM.Features.ExplorationSystem.Nodes;

    public class NodeInteractiveEditor : Editor
    {
        private void OnSceneGUI()
        {
            NodeInteractive nodeInteractive = target as NodeInteractive;

            Debug.Log(Selection.activeGameObject);
            //if (Selection.activeGameObject == nodeInteractive.gameObject)
            //{
            //    nodeInteractive.IsSelected = true;
            //}
        }
    }
}
