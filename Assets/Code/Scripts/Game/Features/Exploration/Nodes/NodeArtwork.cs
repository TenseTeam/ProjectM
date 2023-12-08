namespace ProjectM.Features.Exploration
{
    using UnityEngine;
    using VUDK.Features.More.ExplorationSystem.Nodes;
    using ProjectM.Features.Artwork;

    public class NodeArtwork : NodeView
    {
        [Header("Node Artwork Settings")]
        [SerializeField]
        private ArtworkInfo _artwork;

        public override void OnNodeEnter()
        {
            base.OnNodeEnter();
            _artwork.EnableCanvas();
        }

        public override void OnNodeExit()
        {
            base.OnNodeExit();
            _artwork.DisableAll();
        }

#if UNITY_EDITOR
        protected override void DrawLabel()
        {
            UnityEditor.Handles.Label(transform.position, "-Artwork View");
        }
#endif
    }
}
