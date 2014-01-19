using System;
using System.Collections.Generic;

namespace GRM.Logic.ProgressTracking.Entities
{
    public class StepInfo
    {
        public string Name { get; set; }

        public TimeSpan Duration { get; set; }

        public IEnumerable<SubstepInfo> Substeps { get; set; }
    }
}
