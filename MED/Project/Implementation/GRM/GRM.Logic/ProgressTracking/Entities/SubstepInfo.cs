using System;

namespace GRM.Logic.ProgressTracking.Entities
{
    public class SubstepInfo
    {
        public string Name { get; set; }

        public int EntersCount { get; set; }

        public TimeSpan TotalDuration { get; set; }
    }
}