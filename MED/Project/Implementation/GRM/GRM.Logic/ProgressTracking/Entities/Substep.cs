using System.Diagnostics;

namespace GRM.Logic.ProgressTracking.Entities
{
    public class Substep
    {
        private readonly string _name;
        private int _entersCount;
        private readonly Stopwatch _stopwatch = new Stopwatch();

        public Substep(string name)
        {
            _name = name;
        }

        public void Enter()
        {
            _entersCount++;
            _stopwatch.Start();
        }

        public void Leave()
        {
            _stopwatch.Stop();
        }

        public SubstepInfo GetInfo()
        {
            return new SubstepInfo
                {
                    Name = _name,
                    EntersCount = _entersCount,
                    TotalDuration = _stopwatch.Elapsed
                };
        }
    }
}