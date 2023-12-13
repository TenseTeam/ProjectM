namespace VUDK.Generic.Structures.Grid
{
    using UnityEngine;
    using UnityEngine.UI;
    using VUDK.Generic.Structures.Grid.Bases;

    [RequireComponent(typeof(GridLayoutGroup))]
    public abstract class LayoutGrid<T> : GridBase<T> where T : GridTileBase
    {
        protected RectTransform RectTransform => transform as RectTransform;

        public override void GenerateGrid()
        {
            base.GenerateGrid();
            RectTransform.sizeDelta = new Vector2(Size.x, Size.y);
        }
    }
}