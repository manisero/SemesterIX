using GRM.Logic.ProgressTracking.Entities;

namespace GRM.Logic.ProgressTracking.ProgressTrackers
{
    public class EmptyProgressTracker : IProgressTracker
    {
        public virtual void BeginTask()
        {
        }

        public virtual void EndTask()
        {
        }

        public virtual void BeginStep(string step)
        {
        }

        public virtual void EndStep()
        {
        }

        public virtual void EnterSubstep(string substep)
        {
        }

        public virtual void LeaveSubstep(string substep)
        {
        }

        public virtual TaskInfo GetInfo()
        {
            return null;
        }
    }
}
