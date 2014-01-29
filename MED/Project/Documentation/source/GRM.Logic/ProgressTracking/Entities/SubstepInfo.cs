using System;

namespace GRM.Logic.ProgressTracking.Entities
{
    public class SubstepInfo
    {
        public string Name { get; set; }

        public int EntersCount { get; set; }

        public TimeSpan TotalDuration { get; set; }

        public override string ToString()
        {
            return string.Format("{0}: {1}", Name, TotalDuration);
        }
    }
}