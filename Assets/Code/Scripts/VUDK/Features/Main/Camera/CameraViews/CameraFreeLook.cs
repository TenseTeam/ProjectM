namespace VUDK.Features.Main.Camera.CameraViews
{
    using UnityEngine;
    using VUDK.Features.Main.InputSystem;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class CameraFreeLook : MonoBehaviour
    {
        [Header("Sensitivity Settings")]
        [SerializeField, Range(1f, 100f)]
        protected float _sensitivity = 2f;
        [SerializeField, Range(0f, 1f)]
        private float _sensitivityCoefficient = 0.1f;
        [SerializeField]
        private bool _hasSmoothing;
        [SerializeField]
        private float _smoothTime = 0.1f;

        [Header("Angles Settings")]
        [Tooltip("How far in degrees you can move the camera up")]
        [SerializeField]
        private float _topClamp = 90.0f;
        [Tooltip("How far in degrees you can move the camera down")]
        [SerializeField]
        private float _bottomClamp = -90.0f;

        protected Vector2 LookRotation;
        protected float ClampSens => _sensitivity * _sensitivityCoefficient / 100f;

        protected Camera Camera { get; private set; }

        protected virtual void Awake()
        {
            TryGetComponent(out Camera camera);
            Camera = camera;
        }

        protected virtual void LateUpdate()
        {
            SetLookDirection();
            LookRotate();
        }

        private void SetLookDirection()
        {
            Vector2 _lookDirection = InputsManager.Inputs.Camera.Look.ReadValue<Vector2>();
            float mouseX = _lookDirection.x * ClampSens;
            float mouseY = _lookDirection.y * ClampSens;

            LookRotation.y += mouseX;
            LookRotation.x -= mouseY;
            LookRotation.x = Mathf.Clamp(LookRotation.x, _bottomClamp, _topClamp);
        }

        protected virtual void LookRotate()
        {
            Quaternion targetRotation = Quaternion.Euler(LookRotation.x, LookRotation.y, 0f);

            if(_hasSmoothing)
                transform.localRotation = Quaternion.Slerp(transform.localRotation, targetRotation, Time.deltaTime * _smoothTime);
            else
                transform.localRotation = targetRotation;
        }
    }
}
