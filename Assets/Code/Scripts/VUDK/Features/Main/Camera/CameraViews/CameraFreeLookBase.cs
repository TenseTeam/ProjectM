namespace VUDK.Features.Main.Camera.CameraViews
{
    using UnityEngine;
    using VUDK.Features.Main.InputSystem;

    [RequireComponent(typeof(Camera))]
    public class CameraFreeLookBase : MonoBehaviour
    {

        [SerializeField, Range(1f, 100f), Header("Camera Settings")]
        protected float Sensitivity = 2f;
        [SerializeField, Range(0f, 1f)]
        private float SensitivityCoefficient = 0.1f;
        [Tooltip("How far in degrees you can move the camera up")]
        public float TopClamp = 90.0f;
        [Tooltip("How far in degrees you can move the camera down")]
        public float BottomClamp = -90.0f;

        protected Vector2 LookRotation;
        protected float ClampSensCoef => SensitivityCoefficient / 100f;

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
            float mouseX = _lookDirection.x * Sensitivity * ClampSensCoef;
            float mouseY = _lookDirection.y * Sensitivity * ClampSensCoef;

            LookRotation.y += mouseX;
            LookRotation.x -= mouseY;
            LookRotation.x = Mathf.Clamp(LookRotation.x, BottomClamp, TopClamp);
        }

        protected virtual void LookRotate()
        {
            transform.localRotation = Quaternion.Euler(LookRotation.x, LookRotation.y, 0f);
        }
    }
}
