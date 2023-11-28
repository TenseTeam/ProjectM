namespace ProjectM.Features.Player
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using ProjectM.Features.ExplorationSystem;
    using ProjectM.Constants;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectM.Managers;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(PathExplorer))]
    public class PlayerManager : MonoBehaviour, ICastGameManager<GameManager>
    {
        [field: SerializeField]
        public PlayerCamera PlayerCamera { get; private set; }

        public PathExplorer PathExplorer { get; private set; }

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;

        private void OnValidate()
        {
            if (PlayerCamera == null) PlayerCamera = FindAnyObjectByType<PlayerCamera>();
        }

        private void Awake()
        {
            TryGetComponent(out PathExplorer pathExplorer);
            PathExplorer = pathExplorer;

            PlayerCamera.Init(GameManager.ExplorationManager);
        }
    }
}
