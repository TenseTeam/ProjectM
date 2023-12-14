namespace VUDK.Features.More.ExplorationSystem.Explorers
{
    using VUDK.Features.More.ExplorationSystem.Transition;
    using VUDK.Features.More.ExplorationSystem.Transition.Phases.Keys;

    public class PathExplorer : TransitionMachine
    {
        public void TransitionStart()
        {
            ChangeState(TransitionStateKey.Start);
        }
    }
}
