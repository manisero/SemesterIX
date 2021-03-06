﻿using System.Collections.Generic;
using System.Linq;
using GRM.Logic.ProgressTracking.Entities;

namespace GRM.Logic.ProgressTracking.ProgressTrackers
{
    public class StepProgressTracker : TaskProgressTracker
    {
        protected readonly IList<Step> Steps = new List<Step>();
        protected Step CurrentStep;

        public override void BeginStep(string step)
        {
            CurrentStep = new Step(step);
            Steps.Add(CurrentStep);

            CurrentStep.Stopwatch.Reset();
            CurrentStep.Stopwatch.Start();
        }

        public override void EndStep()
        {
            CurrentStep.Stopwatch.Stop();
        }

        public override TaskInfo GetInfo()
        {
            return new TaskInfo
            {
                Duration = TaskStopwatch.Elapsed,
                Steps = Steps.Select(x => x.GetInfo())
            };
        }
    }
}
