using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GRM.Logic.ProgressTracking
{
    public class ProgressInfo
    {
        private class Substep
        {
            private int _entersCount;
            private readonly Stopwatch _stopwatch;

            public Substep()
            {
                _entersCount = 0;
                _stopwatch = new Stopwatch();
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
                        EntersCount = _entersCount,
                        TotalDuration = _stopwatch.Elapsed
                    };
            }
        }

        public class SubstepInfo
        {
            public int EntersCount { get; set; }
            public TimeSpan TotalDuration { get; set; }
        }

        private string _step;
        private readonly Action<string> _onStepStart;
        private readonly Action<string, TimeSpan> _onStepEnd;
        private readonly Stopwatch _taskStopwatch = new Stopwatch();
        private readonly Stopwatch _stepStopwatch = new Stopwatch();
        private readonly IDictionary<string, Substep> _substepStopwatches = new Dictionary<string, Substep>();

        public ProgressInfo()
        {
        }

        public ProgressInfo(Action<string> onStepStart, Action<string, TimeSpan> onStepEnd)
        {
            _onStepStart = onStepStart;
            _onStepEnd = onStepEnd;
        }

        public void BeginTask()
        {
            _taskStopwatch.Start();
        }

        public void EndTask()
        {
            _taskStopwatch.Stop();
        }

        public void BeginStep(string step)
        {
            _stepStopwatch.Stop();
            _stepStopwatch.Reset();

            _step = step;

            if (_onStepStart != null)
            {
                _onStepStart(_step);
            }

            _stepStopwatch.Start();
        }

        public void EndStep()
        {
            _stepStopwatch.Stop();

            if (_onStepEnd != null)
            {
                _onStepEnd(_step, _stepStopwatch.Elapsed);
            }

            _step = null;
        }

        public void EnterSubstep(string substep)
        {
            if (!_substepStopwatches.ContainsKey(substep))
            {
                _substepStopwatches.Add(substep, new Substep());
            }

            _substepStopwatches[substep].Enter();
        }

        public void LeaveSubstep(string substep)
        {
            _substepStopwatches[substep].Leave();
        }

        public IDictionary<string, SubstepInfo> GetSubstepsDurations()
        {
            return _substepStopwatches.ToDictionary(x => x.Key, x => x.Value.GetInfo());
        }

        public TimeSpan GetOverallTaskDuration()
        {
            return _taskStopwatch.Elapsed;
        }
    }
}
