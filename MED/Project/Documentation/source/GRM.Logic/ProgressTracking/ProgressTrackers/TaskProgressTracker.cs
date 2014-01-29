using System.Diagnostics;
using GRM.Logic.ProgressTracking.Entities;

namespace GRM.Logic.ProgressTracking.ProgressTrackers
{
    public class TaskProgressTracker : EmptyProgressTracker
    {
        protected readonly Stopwatch TaskStopwatch = new Stopwatch();

        public override void BeginTask()
        {
            TaskStopwatch.Start();
        }

        public override void EndTask()
        {
            TaskStopwatch.Stop();
        }

        public override TaskInfo GetInfo()
        {
            return new TaskInfo
                {
                    Duration = TaskStopwatch.Elapsed
                };
        }
    }
}
