using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GRM.Logic
{
    public class ProgressInfo
    {
        private string _step;
        private readonly Action<string> _onStepStart;
        private readonly Action<string, TimeSpan> _onStepEnd;
        private readonly Stopwatch _taskStopwatch = new Stopwatch();
        private readonly Stopwatch _stepStopwatch = new Stopwatch();
        private readonly IDictionary<string, Stopwatch> _substepStopwatches = new Dictionary<string, Stopwatch>();

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
                _substepStopwatches.Add(substep, new Stopwatch());
            }

            _substepStopwatches[substep].Start();
        }

        public void LeaveSubstep(string substep)
        {
            _substepStopwatches[substep].Stop();
        }

        public IDictionary<string, TimeSpan> GetSubstepsDurations()
        {
            return _substepStopwatches.ToDictionary(x => x.Key, x => x.Value.Elapsed);
        }

        public TimeSpan GetOverallTaskDuration()
        {
            return _taskStopwatch.Elapsed;
        }
    }
}
