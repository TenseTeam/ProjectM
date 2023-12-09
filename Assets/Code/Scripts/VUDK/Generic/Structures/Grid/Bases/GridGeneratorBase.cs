namespace VUDK.Generic.Structures.Grid.Bases
{
    using UnityEngine;

    public abstract class GridGeneratorBase : MonoBehaviour
    {
        public abstract void GenerateGrid();
        public abstract void ClearGrid();
    }
}