namespace ProjectM.Features.ExplorationSystem
{
    using ProjectM.Features.ExplorationSystem.Transition;
    using ProjectM.Features.ExplorationSystem.Transition.Phases.Keys;

    public class PathExplorer : TransitionMachine
    {
        public void TransitionStart()
        {
            ChangeState(TransitionStateKey.Start);
        }
    }
}
