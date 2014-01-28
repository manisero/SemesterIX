using GRM.Logic.ProgressTracking.Entities;

namespace GRM.Logic.ProgressTracking
{
    public interface IProgressTracker
    {
        void BeginTask();
        void EndTask();

        void BeginStep(string step);
        void EndStep();

        int RegisterSubstep(string substepName);
        void EnterSubstep(int substepId);
        void LeaveSubstep(int substepId);

        TaskInfo GetInfo();
    }
}