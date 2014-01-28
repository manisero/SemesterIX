using System;
using System.Collections.Generic;

namespace GRM.Logic.ProgressTracking.Entities
{
    public class StepInfo
    {
        public string Name { get; set; }

        public TimeSpan Duration { get; set; }

        public IEnumerable<SubstepInfo> Substeps { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, Duration);
        }
    }
}
