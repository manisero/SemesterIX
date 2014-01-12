using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GRM.Logic.ProgressTracking
{
    public class Step
    {
        private readonly string _name;
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly IDictionary<string, Substep> _substeps = new Dictionary<string, Substep>();

        public Step(string name)
        {
            _name = name;
        }

        public void Begin()
        {
            _stopwatch.Reset();
            _stopwatch.Start();
        }

        public void End()
        {
            _stopwatch.Stop();
        }

        public void EnterSubstep(string substep)
        {
            if (!_substeps.ContainsKey(substep))
            {
                _substeps.Add(substep, new Substep(substep));
            }

            _substeps[substep].Enter();
        }

        public void LeaveSubstep(string substep)
        {
            _substeps[substep].Leave();
        }

        public StepInfo GetInfo()
        {
            return new StepInfo
                {
                    Name = _name,
                    Duration = _stopwatch.Elapsed,
                    Substeps = _substeps.Values.Select(x => x.GetInfo())
                };
        }
    }
}
