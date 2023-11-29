namespace ProjectM.Features.Exploration
{
    using UnityEngine;
    using VUDK.Features.Packages.ExplorationSystem.Nodes;
    using ProjectM.Features.Artwork;

    public class NodeArtwork : NodeObservation
    {
        [Header("Node Artwork Settings")]
        [SerializeField]
        private Artwork _artwork;

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
