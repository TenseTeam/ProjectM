namespace ProjectM.Game.Features.Artwork.UI
{
    using TMPro;
    using UnityEngine;
    using VUDK.Patterns.Initialization.Interfaces;
    using ProjectM.Features.Artwork.Data;

    public class UIArtworkInfoBox : MonoBehaviour, IInit
    {
        [Header("Info Settings")]
        [SerializeField]
        private TMP_Text _name;

        [SerializeField]
        private TMP_Text _description;

        [field: Header("Artwork Data")]
        [field: SerializeField]
        protected ArtworkInfoData ArtworkInfoData { get; private set; }

        private void OnValidate()
        {
            if (Check()) Init();
        }

        private void Awake()
        {
            Init();
        }

        public void Init()
        {
            _name.text = ArtworkInfoData.Name;
            _description.text = ArtworkInfoData.Description;
        }

        public virtual bool Check()
        {
            return ArtworkInfoData != null;
        }

        public void Enable()
        {
            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}