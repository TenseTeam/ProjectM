namespace VUDK.Generic.Structures.Grid
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Structures.Grid.Bases;

    [RequireComponent(typeof(GridLayoutGroup))]
    public abstract class LayoutGrid<T> : GridBase<T> where T : GridTileBase
    {
        private RectTransform rectTransform => transform as RectTransform;

        public override void GenerateGrid()
        {
            base.GenerateGrid();
            rectTransform.sizeDelta = new Vector2(Size.x, Size.y);
        }
    }
}