namespace VUDK.Features.Packages.ExplorationSystem.Explorers
{
    using VUDK.Features.Packages.ExplorationSystem.Transition;
    using VUDK.Features.Packages.ExplorationSystem.Transition.Phases.Keys;

    public class PathExplorer : TransitionMachine
    {
        public void TransitionStart()
        {
            ChangeState(TransitionStateKey.Start);
        }
    }
}
