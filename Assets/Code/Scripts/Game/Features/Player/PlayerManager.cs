namespace ProjectM.Features.Player
{
    using UnityEngine;
    using VUDK.Generic.Managers.Main;
    using VUDK.Features.More.ExplorationSystem.Managers;
    using VUDK.Features.More.ExplorationSystem.Constants;
    using VUDK.Features.More.ExplorationSystem.Explorers;
    using VUDK.Features.Main.EventSystem;
    using VUDK.Patterns.Initialization.Interfaces;

    [DisallowMultipleComponent]
    [RequireComponent(typeof(PathExplorer))]
    public class PlayerManager : MonoBehaviour, IInit<ExplorationManager>
    {
        [field: SerializeField]
        public PlayerCamera PlayerCamera { get; private set; }

        public PathExplorer PathExplorer { get; private set; }

        private void OnValidate()
        {
            if (PlayerCamera == null) PlayerCamera = FindAnyObjectByType<PlayerCamera>();
        }

        private void OnEnable()
        {
            EventManager.Ins.AddListener<ExplorationManager>(ExplorationEventKeys.OnExplorationManagerInit, Init);
        }

        private void OnDisable()
        {
            EventManager.Ins.RemoveListener<ExplorationManager>(ExplorationEventKeys.OnExplorationManagerInit, Init);
        }

        private void Awake()
        {
            TryGetComponent(out PathExplorer pathExplorer);
            PathExplorer = pathExplorer;
        }

        /// <inheritdoc/>
        public void Init(ExplorationManager explorationManager)
        {
            PlayerCamera.Init(explorationManager);
        }

        /// <inheritdoc/>
        public bool Check()
        {
            return PlayerCamera.Check();
        }
    }
}
