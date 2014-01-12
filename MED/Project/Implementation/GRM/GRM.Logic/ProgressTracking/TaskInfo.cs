using System;
using System.Collections.Generic;

namespace GRM.Logic.ProgressTracking
{
    public class TaskInfo
    {
        public TimeSpan Duration { get; set; }

        public IEnumerable<StepInfo> Steps { get; set; }
    }
}
