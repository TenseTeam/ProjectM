namespace VUDK.Features.Main.Camera.CameraViews
{
    using UnityEngine;
    using VUDK.Extensions;
    using VUDK.Features.Main.InputSystem;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(Camera))]
    public class CameraFreeLook : MonoBehaviour
    {
        public Vector3 TargetRotation;

        [Header("Sensitivity Settings")]
        [SerializeField, Range(1f, 100f)]
        protected float _sensitivity = 2f;
        [SerializeField, Range(0f, 1f)]
        private float _sensitivityCoefficient = 0.1f;
        [SerializeField]
        private float _smoothTime = 0.1f;

        [Header("Angles Settings")]
        [Tooltip("How far in degrees you can move the camera up")]
        [SerializeField]
        private float _topClamp = 90.0f;
        [Tooltip("How far in degrees you can move the camera down")]
        [SerializeField]
        private float _bottomClamp = -90.0f;

        protected float ClampSens => _sensitivity * _sensitivityCoefficient / 100f;
        protected Camera Camera { get; private set; }

        private bool _canRotate = true;

        protected virtual void Awake()
        {
            TryGetComponent(out Camera camera);
            Camera = camera;
        }

        public virtual void Enable()
        {
            InputsManager.Inputs.Camera.Enable();
            _canRotate = true;
        }

        public virtual void Disable()
        {
            InputsManager.Inputs.Camera.Disable();
            _canRotate = false;
        }

        public virtual void ResetTargetRotation()
        {
            TargetRotation = Vector3.zero;
        }

        protected virtual void LateUpdate()
        {
            LookRotate();
        }

        public void SetRotation(Quaternion rotation)
        {
            TargetRotation = rotation.SignedEulerAngles();
        }

        protected virtual void LookRotate()
        {
            if(!_canRotate) return;

            Vector2 _lookDirection = InputsManager.Inputs.Camera.Look.ReadValue<Vector2>();
            float mouseX = _lookDirection.x * ClampSens;
            float mouseY = _lookDirection.y * ClampSens;

            TargetRotation.y += mouseX;
            TargetRotation.x -= mouseY;
            TargetRotation.x = Mathf.Clamp(TargetRotation.x, _bottomClamp, _topClamp);

            transform.rotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(TargetRotation), Time.deltaTime * _smoothTime);
        }
    }
}
