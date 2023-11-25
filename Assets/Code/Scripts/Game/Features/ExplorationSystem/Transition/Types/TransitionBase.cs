namespace ProjectM.Features.ExplorationSystem.Transition.Types
{
    using System;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectM.Features.ExplorationSystem.Nodes;
    using ProjectM.Managers;
    using UnityEngine;
    using ProjectM.Features.Player;

    public abstract class TransitionBase : ICastGameManager<GameManager>
    {
        public Action OnTransitionCompleted;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        protected GameStats GameStats => MainManager.Ins.GameStats;
        protected PathExplorer PathExplorer => GameManager.ExplorationManager.PathExplorer;
        protected NodeBase TargetNode => GameManager.ExplorationManager.CurrentTargetNode;
        protected PlayerCamera PlayerCamera { get; private set; }

        public TransitionBase()
        {
            PlayerCamera = GameStats.PlayerCamera.TryGetComponent(out PlayerCamera playerCamera) ? playerCamera : null;
        }

        public virtual void Begin()
        {
        }

        public virtual void Process()
        {
        }

        public virtual void End()
        {
        }

        public virtual void OnTransitionCompletedHandler()
        {
            OnTransitionCompleted?.Invoke();
        }
    }
}
