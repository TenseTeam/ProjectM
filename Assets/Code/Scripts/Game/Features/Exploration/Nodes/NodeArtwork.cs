namespace ProjectM.Features.Exploration
{
    using UnityEngine;
    using VUDK.Features.More.ExplorationSystem.Nodes;
    using ProjectM.Features.Artwork;

    public class NodeArtwork : NodeView
    {
        [Header("Artwork Info")]
        [SerializeField]
        private ArtworkInfo _artworkInfo;

        public override void OnNodeEnter()
        {
            base.OnNodeEnter();
            _artworkInfo.EnableCanvas();
        }

        public override void OnNodeExit()
        {
            base.OnNodeExit();
            _artworkInfo.DisableAll();
        }

#if UNITY_EDITOR
        /// <summary>
        /// Draws the label of the node.
        /// </summary>
        protected override void DrawLabel()
        {
            UnityEditor.Handles.Label(transform.position, "-Artwork View");
        }
#endif
    }
}
