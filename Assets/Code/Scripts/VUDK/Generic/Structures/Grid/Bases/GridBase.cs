namespace VUDK.Generic.Structures.Grid.Bases
{
    using UnityEngine;

    public abstract class GridBase<T> : GridGeneratorBase where T : GridTileBase
    {
        [Header("Tile")]
        [SerializeField]
        private GameObject _tilePrefab;

        [field: SerializeField, Min(0)]
        public Vector2Int Size { get; private set; }

        public T[,] GridTiles { get; private set; }

        /// <summary>
        /// Generates a grid of T components.
        /// </summary>
        /// <returns>Grid of T components.</returns>
        public override void GenerateGrid()
        {
            ClearGrid();

            T[,] tiles = new T[Size.x, Size.y];

            for (int y = 0; y < Size.y; y++)
            {
                for (int x = 0; x < Size.x; x++)
                {
                    GenerateTile(tiles, new Vector2Int(x, y));
                }
            }

            GridTiles = tiles;
        }

        public override void ClearGrid()
        {
            GridTiles = null;

            Transform[] children = transform.GetComponentsInChildren<Transform>();

            for(int i = 1; i < children.Length; i++)
            {
                DestroyImmediate(children[i].gameObject);
            }
        }

        /// <summary>
        /// Checks if two cells in the grid are vertically or horizontally adjacent.
        /// </summary>
        /// <param name="positionTileA">Position of the Tile A.</param>
        /// <param name="positionTileB">Position of the Tile B.</param>
        /// <returns>True if they are adjacent, False if they are not.</returns>
        public bool CheckTileAdjacency(Vector2Int positionTileA, Vector2Int positionTileB)
        {
            return IsTileVerticallyAdjacent(positionTileA, positionTileB) || IsTileHorizontallyAdjacent(positionTileA, positionTileB);
        }

        /// <summary>
        /// Instantiates a TilePrefab GameObject and attempts to assign the T component to a grid cell.
        /// </summary>
        /// <param name="grid">Grid of T components.</param>
        /// <param name="gridPosition">Grid cell position.</param>
        /// <returns>T generated tile's component.</returns>
        protected void GenerateTile(T[,] grid, Vector2Int gridPosition)
        {
            GameObject tilePrefab = Instantiate(_tilePrefab, transform.position, transform.rotation, transform);

            if (tilePrefab.TryGetComponent(out T tile))
            {
                grid[gridPosition.x, gridPosition.y] = tile;
                InitTile(tile, gridPosition);
            }
            else
            {
                Debug.LogError($"TilePrefab does not have a {typeof(T)} component.");
            }
        }

        protected virtual void InitTile(T tile, Vector2Int gridPosition)
        {
            tile.Init(gridPosition);
        }

        /// <summary>
        /// Checks if the grid is full.
        /// </summary>
        /// <returns>True if it is full, False if it is not.</returns>
        public bool IsGridFull()
        {
            foreach (T tile in GridTiles)
            {
                if (tile == null) return false;
            }

            return true;
        }

        public bool IsGridEmpty()
        {
            if (GridTiles == null) return true;

            foreach (T tile in GridTiles)
            {
                if (tile != null) return false;
            }

            return true;
        }

        /// <summary>
        /// Checks if two cells in the grid are vertically adjacent.
        /// </summary>
        /// <param name="positionTileA">Position of the Tile A.</param>
        /// <param name="positionTileB">Position of the Tile B.</param>
        /// <returns>True if they are adjacent, False if they are not.</returns>
        protected bool IsTileVerticallyAdjacent(Vector2Int positionTileA, Vector2Int positionTileB)
        {
            int p1 = Mathf.Max(positionTileA.x, positionTileB.x);
            int p2 = Mathf.Min(positionTileA.x, positionTileB.x);

            return p1 - p2 == 1 && positionTileB.y - positionTileA.y == 0;
        }

        /// <summary>
        /// Checks if two cells in the grid are horizontally adjacent.
        /// </summary>
        /// <param name="positionTileA">Position of the Tile A.</param>
        /// <param name="positionTileB">Position of the Tile B.</param>
        /// <returns>True if they are adjacent, False if they are not.</returns>
        protected bool IsTileHorizontallyAdjacent(Vector2Int positionTileA, Vector2Int positionTileB)
        {
            int p1 = Mathf.Max(positionTileA.y, positionTileB.y);
            int p2 = Mathf.Min(positionTileA.y, positionTileB.y);

            return p1 - p2 == 1 && positionTileB.x - positionTileA.x == 0;
        }
    }
}