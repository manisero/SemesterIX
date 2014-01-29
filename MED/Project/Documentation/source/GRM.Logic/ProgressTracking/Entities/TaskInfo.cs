using System;
using System.Collections.Generic;

namespace GRM.Logic.ProgressTracking.Entities
{
    public class TaskInfo
    {
        public TimeSpan Duration { get; set; }

        public IEnumerable<StepInfo> Steps { get; set; }
    }
}
