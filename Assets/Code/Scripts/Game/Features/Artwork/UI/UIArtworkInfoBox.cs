namespace ProjectM.Game.Features.Artwork.UI
{
    using UnityEngine;
    using UnityEngine.Localization;
    using TMPro;
    using VUDK.Patterns.Initialization.Interfaces;
    using ProjectM.Features.Artwork.Data;
    using System;

    public class UIArtworkInfoBox : MonoBehaviour, IInit
    {
        [Header("Info Settings")]
        [SerializeField]
        private TMP_Text _name;

        [SerializeField]
        private TMP_Text _description;

        [Header("Artwork Data")]
        [SerializeField]
        private LocalizedAsset<ArtworkInfoData> _artworkInfoData;

        private ArtworkInfoData _loadedInfoData;

        private void OnValidate()
        {
            if (Check()) Init();
        }

        private void OnEnable()
        {
            _artworkInfoData.AssetChanged += LoadArtworkInfo;
        }

        private void OnDisable()
        {
            _artworkInfoData.AssetChanged -= LoadArtworkInfo;
        }

        private void LoadArtworkInfo(ArtworkInfoData asset)
        {
            _loadedInfoData = asset;
            Init();
        }

        public void Init()
        {
            _name.text = _loadedInfoData.Name;
            _description.text = _loadedInfoData.Description;
        }

        public virtual bool Check()
        {
            return _artworkInfoData != null;
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