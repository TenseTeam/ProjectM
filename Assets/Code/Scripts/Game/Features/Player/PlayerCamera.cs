namespace ProjectM.Features.Player
{
    using VUDK.Features.Main.Camera.CameraViews;
    using VUDK.Features.Main.InputSystem;

    public class PlayerCamera : CameraFreeLook
    {
        public void Enable()
        {
            InputsManager.Inputs.Camera.Enable();
        }

        public void Disable()
        {
            InputsManager.Inputs.Camera.Disable();
        }
    }
}
