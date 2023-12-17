namespace ProjectM.Features.Puzzles.Machine
{
    using ProjectM.Features.Puzzles.Machine.States;
    using ProjectM.Patterns.Factories;
    using VUDK.Patterns.StateMachine;
    using ProjectM.Features.Puzzles.Puzzle15;
    using ProjectM.Features.Puzzles.Puzzle15.Factories;

    public class Game15Machine : StateMachine
    {
        private Game15MachineContext _context;

        public void Init(Game15Puzzle puzzle)
        {
            _context = Game15Factory.Create(puzzle);
            Init();
        }

        public override void Init()
        {
            if (!Check()) return;

            MovePhase movePhase = Game15Factory.Create(Game15PhaseKey.MovePhase, this, _context) as MovePhase;
            CheckPhase checkPhase = Game15Factory.Create(Game15PhaseKey.CheckPhase, this, _context) as CheckPhase;
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
