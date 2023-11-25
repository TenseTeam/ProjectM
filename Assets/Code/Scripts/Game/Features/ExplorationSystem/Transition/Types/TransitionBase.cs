namespace ProjectM.Features.ExplorationSystem.Transition.Types
{
    using System;
    using VUDK.Generic.Managers.Main;
    using VUDK.Generic.Managers.Main.Interfaces;
    using ProjectM.Features.ExplorationSystem.Nodes;
    using ProjectM.Managers;
    using UnityEngine;

    public abstract class TransitionBase : ICastGameManager<GameManager>
    {
        public Action OnTransitionCompleted;

        public GameManager GameManager => MainManager.Ins.GameManager as GameManager;
        protected GameStats GameStats => MainManager.Ins.GameStats;
        protected Camera PlayerCamera => GameStats.PlayerCamera; 
        protected PathExplorer PathExplorer => GameManager.ExplorationManager.PathExplorer;
        protected NodeBase TargetNode => GameManager.ExplorationManager.CurrentTargetNode;


        public TransitionBase()
        {
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
