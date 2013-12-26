using System;
using System.Diagnostics;

namespace GRM.Logic.DataSetProcessing
{
    public class ProgressInfo
    {
        private string _step;
        private readonly Action<string> _onStepStart;
        private readonly Action<string, TimeSpan> _onStepEnd;
        private readonly Stopwatch _taskStopwatch = new Stopwatch();
        private readonly Stopwatch _stepStopwatch = new Stopwatch();

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

        public void EndTask()
        {
            _taskStopwatch.Stop();
        }

        public TimeSpan GetOverallTaskDuration()
        {
            return _taskStopwatch.Elapsed;
        }
    }
}
