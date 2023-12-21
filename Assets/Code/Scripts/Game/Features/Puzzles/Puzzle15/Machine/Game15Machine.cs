namespace ProjectM.Features.Puzzles.Machine
{
    using VUDK.Patterns.StateMachine;
    using ProjectM.Features.Puzzles.Machine.States;
    using ProjectM.Features.Puzzles.Puzzle15.Factories;

    public class Game15Machine : StateMachine
    {
        private Game15MachineContext _context;

        /// <summary>
        /// Initializes the machine.
        /// </summary>
        /// <param name="puzzle">Puzzle.</param>
        public void Init(Game15Puzzle puzzle)
        {
            _context = Game15Factory.Create(puzzle);
            Init();
        }

        /// <inheritdoc/>
        public override void Init()
        {
            if (!Check()) return;

            MovePhase movePhase = Game15Factory.Create(Game15PhaseKey.MovePhase, this, _context) as MovePhase;
            CheckPhase checkPhase = Game15Factory.Create(Game15PhaseKey.CheckPhase, this, _context) as CheckPhase;
            AddState(Game15PhaseKey.MovePhase, movePhase);
            AddState(Game15PhaseKey.CheckPhase, checkPhase);
        }
        
        /// <inheritdoc/>
        public override bool Check()
        {
            return _context != null;
        }

        /// <summary>
        /// Starts the machine.
        /// </summary>
        public void StartMachine()
        {
            if (Check())
                ChangeState(Game15PhaseKey.MovePhase);
        }
    }
}
