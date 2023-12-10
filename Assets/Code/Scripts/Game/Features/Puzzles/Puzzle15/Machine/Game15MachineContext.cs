namespace ProjectM.Features.Puzzles.Puzzle15.Machine
{
    using VUDK.Patterns.StateMachine;

    public class Game15MachineContext : StateContext
    {
        public Game15Puzzle Puzzle { get; private set; }

        public Game15MachineContext(Game15Puzzle puzzle)
        {
            Puzzle = puzzle;
        }
    }
}
