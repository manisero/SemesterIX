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

        public virtual int RegisterSubstep(string substepName)
        {
            return 0;
        }

        public virtual void EnterSubstep(int substepId)
        {
        }

        public virtual void LeaveSubstep(int substepId)
        {
        }

        public virtual TaskInfo GetInfo()
        {
            return null;
        }
    }
}
