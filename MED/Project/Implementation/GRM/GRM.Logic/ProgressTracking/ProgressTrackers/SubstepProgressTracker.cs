namespace GRM.Logic.ProgressTracking.ProgressTrackers
{
    public class SubstepProgressTracker : StepProgressTracker
    {
        public override void EnterSubstep(string substep)
        {
            CurrentStep.EnterSubstep(substep);
        }

        public override void LeaveSubstep(string substep)
        {
            CurrentStep.LeaveSubstep(substep);
        }
    }
}
