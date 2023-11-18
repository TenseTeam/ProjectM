namespace ProjectM.Debug
{
    using UnityEngine;
    using UnityEngine.InputSystem;
    using VUDK.Features.Main.InputSystem;

    public class DebugMovement : VUDK.Features.Main.CharacterController.CharacterController3DBase
    {
        private void OnEnable()
        {
            InputsManager.Inputs.Player.Movement.performed += Move;
            InputsManager.Inputs.Player.Movement.canceled += Stop;
            InputsManager.Inputs.Player.Jump.performed += Jump;
        }

        private void OnDisable()
        {
            InputsManager.Inputs.Player.Movement.performed -= Move;
            InputsManager.Inputs.Player.Movement.canceled -= Stop;
            InputsManager.Inputs.Player.Jump.performed -= Jump;
        }

        protected override void Update()
        {
            base.Update();
            IsRunning = InputsManager.Inputs.Player.Sprint.IsInProgress();
        }

        private void Move(InputAction.CallbackContext context)
        {
            Vector3 inputDirection = context.ReadValue<Vector2>();
            MoveCharacter(inputDirection);
        }

        private void Jump(InputAction.CallbackContext obj)
        {
            Jump(Vector3.up);
        }

        private void Stop(InputAction.CallbackContext obj)
        {
            StopInputMovementCharacter();
        }
    }
}