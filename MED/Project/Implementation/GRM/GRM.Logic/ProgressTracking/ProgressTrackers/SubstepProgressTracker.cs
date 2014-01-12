using System.Collections.Generic;
using GRM.Logic.ProgressTracking.Entities;

namespace GRM.Logic.ProgressTracking.ProgressTrackers
{
    public class SubstepProgressTracker : StepProgressTracker
    {
        protected readonly IList<string> SubstepsNames = new List<string>();

        public override int RegisterSubstep(string substepName)
        {
            SubstepsNames.Add(substepName);

            return SubstepsNames.Count - 1;
        }

        public override void EnterSubstep(int substepId)
        {
            if (!CurrentStep.Substeps.ContainsKey(substepId))
            {
                CurrentStep.Substeps.Add(substepId, new Substep(SubstepsNames[substepId]));
            }

            var substep = CurrentStep.Substeps[substepId];
            substep.EntersCount++;
            substep.Stopwatch.Start();
        }

        public override void LeaveSubstep(int substepId)
        {
            CurrentStep.Substeps[substepId].Stopwatch.Stop();
        }
    }
}
