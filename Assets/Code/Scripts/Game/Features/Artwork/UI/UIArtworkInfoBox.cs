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
            if (Check())
            {
                _artworkInfoData.AssetChanged += LoadArtworkInfo;
            }
            else
            {
                _artworkInfoData.AssetChanged -= LoadArtworkInfo;
            }
        }

        private void OnEnable()
        {
            _artworkInfoData.AssetChanged += LoadArtworkInfo;
        }

        private void OnDisable()
        {
            _artworkInfoData.AssetChanged -= LoadArtworkInfo;
        }

        /// <summary>
        /// Loads the artwork info data.
        /// </summary>
        /// <param name="asset"><see cref="ArtworkInfoData"/> to load.</param>
        private void LoadArtworkInfo(ArtworkInfoData asset)
        {
            _loadedInfoData = asset;
            Init();
        }

        /// <inheritdoc/>
        public void Init()
        {
            _name.text = _loadedInfoData.Name;
            _description.text = _loadedInfoData.Description;
        }

        /// <inheritdoc/>
        public virtual bool Check()
        {
            return _artworkInfoData != null;
        }

        /// <summary>
        /// Enables the UI element.
        /// </summary>
        public void Enable()
        {
            gameObject.SetActive(true);
        }

        /// <summary>
        /// Disables the UI element.
        /// </summary>
        public void Disable()
        {
            gameObject.SetActive(false);
        }
    }
}