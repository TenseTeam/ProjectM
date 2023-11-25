namespace ProjectM.Features.Player
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectM.Features.ExplorationSystem;
    using ProjectM.Constants;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(PathExplorer))]
    public class PlayerManager : MonoBehaviour
    {
        [SerializeField]
        private PlayerCamera _playerCamera;

        private PathExplorer _pathExplorer;

        private void OnValidate()
        {
            if (_playerCamera == null) _playerCamera = FindAnyObjectByType<PlayerCamera>();
        }

        private void Awake()
        {
            TryGetComponent(out _pathExplorer);
        }

        private void OnEnable()
        {
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnBeginTransition, _playerCamera.Disable);
            MainManager.Ins.EventManager.AddListener(GameEventKeys.OnEndTransition, _playerCamera.Enable);
        }

        private void OnDisable()
        {
            MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnBeginTransition, _playerCamera.Disable);
            MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnEndTransition, _playerCamera.Enable);
        }
    }
}
