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
        [field: SerializeField]
        public PlayerCamera PlayerCamera { get; private set; }

        public PathExplorer PathExplorer { get; private set; }

        private void OnValidate()
        {
            if (PlayerCamera == null) PlayerCamera = FindAnyObjectByType<PlayerCamera>();
        }

        private void Awake()
        {
            TryGetComponent(out PathExplorer pathExplorer);
            PathExplorer = pathExplorer;
        }

        //private void OnEnable()
        //{
        //    MainManager.Ins.EventManager.AddListener(GameEventKeys.OnBeginTransition, PlayerCamera.Disable);
        //    MainManager.Ins.EventManager.AddListener(GameEventKeys.OnEndTransition, PlayerCamera.Enable);
        //}

        //private void OnDisable()
        //{
        //    MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnBeginTransition, PlayerCamera.Disable);
        //    MainManager.Ins.EventManager.RemoveListener(GameEventKeys.OnEndTransition, PlayerCamera.Enable);
        //}
    }
}
