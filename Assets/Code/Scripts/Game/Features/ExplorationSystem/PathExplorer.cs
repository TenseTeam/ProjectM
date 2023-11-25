namespace ProjectM.Features.ExplorationSystem
{
    using ProjectM.Features.ExplorationSystem.Transition;
    using ProjectM.Features.ExplorationSystem.Transition.Phases.Keys;

    public class PathExplorer : TransitionMachine
    {
        public void StartMachine()
        {
            ChangeState(TransitionStateKey.Start);
        }
    }
}
