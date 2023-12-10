namespace ProjectM.Features.Puzzles.Puzzle15.Machine
{
    using ProjectM.Features.Puzzles.Puzzle15.Machine.States;
    using ProjectM.Patterns.Factories;
    using VUDK.Patterns.StateMachine;

    public class Game15Machine : StateMachine
    {
        private Game15MachineContext _context;

        public void Init(Game15Puzzle puzzle)
        {
            _context = new Game15MachineContext(puzzle);
            Init();
        }

        public override void Init()
        {
            if (!Check()) return;

            MovePhase movePhase = MachineFactory.Create(Game15PhaseKey.MovePhase, this, _context) as MovePhase;
            CheckPhase checkPhase = MachineFactory.Create(Game15PhaseKey.CheckPhase, this, _context) as CheckPhase;
            AddState(Game15PhaseKey.MovePhase, movePhase);
            AddState(Game15PhaseKey.CheckPhase, checkPhase);
        }

        public override bool Check()
        {
            return _context != null;
        }

        public void StartMachine()
        {
            if (Check())
                ChangeState(Game15PhaseKey.MovePhase);
        }
    }
}
