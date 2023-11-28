namespace ProjectM.Features.Artwork
{
    using UnityEngine;
    using UnityEngine.UI;
    using TMPro;
    using VUDK.Patterns.Initialization.Interfaces;
    using ProjectM.Features.Artwork.Data;

    public class Artwork : MonoBehaviour, IInit
    {
        [Header("Artwork UI")]
        [SerializeField]
        private Canvas _artworkCanvas;
        [SerializeField]
        private RectTransform _infoPanel;

        [Header("Artwork Texts")]
        [SerializeField]
        protected TMP_Text NameText;
        [SerializeField]
        private TMP_Text DescriptionText;

        [Header("Artwork Buttons")]
        [SerializeField]
        private Button _infoButton;

        [field: Header("Artwork Data")]
        [field: SerializeField]
        protected ArtworkData ArtworkData { get; private set; }

        protected virtual void OnValidate()
        {
            if (Check()) Init();
        }

        protected virtual void Awake()
        {
            Init();
        }

        protected virtual void OnEnable()
        {
            _infoButton.onClick.AddListener(EnableInfoPanel);
        }

        protected virtual void OnDisable()
        {
            _infoButton.onClick.RemoveListener(EnableInfoPanel);
        }

        protected virtual void Start()
        {
            DisableInfoPanel();
            DisableCanvas();
        }

        public virtual void Init()
        {
            NameText.text = ArtworkData.Name;
            DescriptionText.text = ArtworkData.Description;
        }

        public virtual bool Check()
        {
            return NameText != null && DescriptionText != null && ArtworkData != null;
        }

        public void EnableInfoPanel()
        {
            _infoPanel.gameObject.SetActive(true);
            ChangeInfoButtonBehaviour();
        }

        public void DisableInfoPanel()
        {
            _infoPanel.gameObject.SetActive(false);
            ChangeInfoButtonBehaviour();
        }

        public void EnableUIInfo()
        {
            _artworkCanvas.gameObject.SetActive(true);
        }

        public void DisableCanvas()
        {
            _artworkCanvas.gameObject.SetActive(false);
        }

        private void ChangeInfoButtonBehaviour()
        {
            _infoButton.onClick.RemoveAllListeners();

            if (_infoPanel.gameObject.activeSelf)
                _infoButton.onClick.AddListener(DisableInfoPanel);
            else
                _infoButton.onClick.AddListener(EnableInfoPanel);
        }
    }
}
