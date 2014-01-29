using System.Diagnostics;

namespace GRM.Logic.ProgressTracking.Entities
{
    public class Substep
    {
        public string Name { get; set; }
        public int EntersCount { get; set; }
        public Stopwatch Stopwatch { get; set; }

        public Substep(string name)
        {
            Name = name;
            EntersCount = 0;
            Stopwatch = new Stopwatch();
        }

        public SubstepInfo GetInfo()
        {
            return new SubstepInfo
                {
                    Name = Name,
                    EntersCount = EntersCount,
                    TotalDuration = Stopwatch.Elapsed
                };
        }
    }
}