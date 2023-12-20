namespace ProjectM.Utility
{
    using UnityEngine;

    public class ARCameraSwitcher : MonoBehaviour
    {
        [Header("Camera Switcher Settings")]
        [SerializeField]
        private Camera _arCamera;
        [SerializeField]
        private Camera _mainCamera;
        [SerializeField]
        private bool _isMainCameraOnStart = false;

        private void Awake()
        {
            if (_arCamera == null)
                Debug.LogError("AR Camera is not set.");

            if (_mainCamera == null)
                Debug.LogError("Main Camera is not set.");
        }

        private void Start()
        {
            _mainCamera.enabled = _isMainCameraOnStart;
            _arCamera.enabled = !_isMainCameraOnStart;
        }

        [ContextMenu("Switch")]
        public void SwitchCamera()
        {
            _arCamera.enabled = !_arCamera.enabled;
            _mainCamera.enabled = !_mainCamera.enabled;
        }
    }
}
