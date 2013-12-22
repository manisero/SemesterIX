using System;

namespace GRM.Logic.DataSetProcessing
{
    public class ProgressInfo
    {
        private string _step;
        private readonly Action<string> _onStepStart;
        private readonly Action<string, TimeSpan> _onStepEnd;

        public ProgressInfo(Action<string> onStepStart, Action<string, TimeSpan> onStepEnd)
        {
            _onStepStart = onStepStart;
            _onStepEnd = onStepEnd;
        }

        public void BeginTask()
        {
            
        }

        public void BeginStep(string step)
        {
            _step = step;

            _onStepStart(_step);
        }

        public void EndStep()
        {
            _onStepEnd(_step, new TimeSpan());

            _step = null;
        }

        public void EndTask()
        {

        }

        public TimeSpan GetOverallTaskDuration()
        {
            return new TimeSpan();
        }
    }
}
