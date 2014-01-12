using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GRM.Logic.ProgressTracking
{
    public class ProgressTracker
    {
        private readonly Stopwatch _stopwatch = new Stopwatch();
        private readonly IList<Step> _steps = new List<Step>();
        private Step _currentStep;

        public void BeginTask()
        {
            _stopwatch.Start();
        }

        public void EndTask()
        {
            _stopwatch.Stop();
        }

        public void BeginStep(string step)
        {
            _currentStep = new Step(step);
            _steps.Add(_currentStep);

            _currentStep.Begin();
        }

        public void EndStep()
        {
            _currentStep.End();
        }

        public void EnterSubstep(string substep)
        {
            _currentStep.EnterSubstep(substep);
        }

        public void LeaveSubstep(string substep)
        {
            _currentStep.LeaveSubstep(substep);
        }

        public TaskInfo GetInfo()
        {
            return new TaskInfo
                {
                    Duration = _stopwatch.Elapsed,
                    Steps = _steps.Select(x => x.GetInfo())
                };
        }
    }
}
