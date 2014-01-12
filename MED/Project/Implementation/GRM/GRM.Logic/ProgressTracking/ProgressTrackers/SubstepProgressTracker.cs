using GRM.Logic.ProgressTracking.Entities;

namespace GRM.Logic.ProgressTracking.ProgressTrackers
{
    public class SubstepProgressTracker : StepProgressTracker
    {
        public override void EnterSubstep(string substep)
        {
            if (!CurrentStep.Substeps.ContainsKey(substep))
            {
                CurrentStep.Substeps.Add(substep, new Substep(substep));
            }

            CurrentStep.Substeps[substep].EntersCount++;
            CurrentStep.Substeps[substep].Stopwatch.Start();
        }

        public override void LeaveSubstep(string substep)
        {
            CurrentStep.Substeps[substep].Stopwatch.Stop();
        }
    }
}
