namespace ProjectM.Features.Artwork
{
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;
    using ProjectM.Game.Features.Artwork.UI;

    public class ArtworkInfo : MonoBehaviour
    {
        [Header("Artwork UI")]
        [SerializeField]
        protected Canvas ArtworkCanvas;
        [SerializeField]
        protected List<UIArtworkInfoBox> InfoBoxes;

        [Header("Artwork Buttons")]
        [SerializeField]
        protected Button InfoButton;

        private bool _areInfoBoxesEnabled;

        protected virtual void OnEnable()
        {
            InfoButton.onClick.AddListener(EnableInfoPanel);
        }

        protected virtual void OnDisable()
        {
            InfoButton.onClick.RemoveListener(EnableInfoPanel);
        }

        protected virtual void Start()
        {
            DisableAll();
        }

        public virtual void EnableAll()
        {
            EnableInfoPanel();
            EnableCanvas();
        }

        public virtual void DisableAll()
        {
            DisableInfoPanel();
            DisableCanvas();
        }

        public virtual void EnableInfoPanel()
        {
            _areInfoBoxesEnabled = true;
            ChangeInfoButtonBehaviour();
            foreach (var infoBox in InfoBoxes)
                infoBox.Enable();
        }

        public virtual void DisableInfoPanel()
        {
            _areInfoBoxesEnabled = false;
            ChangeInfoButtonBehaviour();
            foreach (var infoBox in InfoBoxes)
                infoBox.Disable();
        }

        public virtual void EnableCanvas()
        {
            ArtworkCanvas.gameObject.SetActive(true);
        }

        public virtual void DisableCanvas()
        {
            ArtworkCanvas.gameObject.SetActive(false);
        }

        private void ChangeInfoButtonBehaviour()
        {
            InfoButton.onClick.RemoveAllListeners();

            if (_areInfoBoxesEnabled)
                InfoButton.onClick.AddListener(DisableInfoPanel);
            else
                InfoButton.onClick.AddListener(EnableInfoPanel);
        }
    }
}