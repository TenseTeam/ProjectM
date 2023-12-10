namespace ProjectM.Patterns.Factories
{
    using VUDK.Patterns.StateMachine;
    using VUDK.Features.More.ExplorationSystem.Transition;
    using VUDK.Features.More.ExplorationSystem.Transition.Phases;
    using VUDK.Features.More.ExplorationSystem.Transition.Phases.Keys;
    using VUDK.Features.More.ExplorationSystem;
    using ProjectM.Features.Puzzles.Puzzle15.Machine;
    using ProjectM.Features.Puzzles.Puzzle15.Machine.States;

    /// <summary>
    /// Factory responsible for creating game-related state-machine states and contexts.
    /// </summary>
    public static class MachineFactory
    {
        public static State<Game15MachineContext> Create(Game15PhaseKey phaseKey, StateMachine relatedMachine, Game15MachineContext context)
        {
            switch (phaseKey)
            {
                case Game15PhaseKey.MovePhase:
                    return new MovePhase(phaseKey, relatedMachine, context);
                case Game15PhaseKey.CheckPhase:
                    return new CheckPhase(phaseKey, relatedMachine, context);
            }

            return null;
        }
    }
}