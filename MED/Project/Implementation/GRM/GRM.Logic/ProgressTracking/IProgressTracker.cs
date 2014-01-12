using GRM.Logic.ProgressTracking.Entities;

namespace GRM.Logic.ProgressTracking
{
    public interface IProgressTracker
    {
        void BeginTask();
        void EndTask();
        void BeginStep(string step);
        void EndStep();
        void EnterSubstep(string substep);
        void LeaveSubstep(string substep);
        TaskInfo GetInfo();
    }
}