using GRM.Logic.ProgressTracking.Entities;

namespace GRM.Logic.ProgressTracking.ProgressTrackers
{
    public class EmptyProgressTracker : IProgressTracker
    {
        public void BeginTask()
        {
        }

        public void EndTask()
        {
        }

        public void BeginStep(string step)
        {
        }

        public void EndStep()
        {
        }

        public void EnterSubstep(string substep)
        {
        }

        public void LeaveSubstep(string substep)
        {
        }

        public TaskInfo GetInfo()
        {
            return null;
        }
    }
}
