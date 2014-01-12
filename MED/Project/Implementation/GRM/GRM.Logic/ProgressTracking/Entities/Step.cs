using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;

namespace GRM.Logic.ProgressTracking.Entities
{
    public class Step
    {
        public string Name { get; set; }
        public Stopwatch Stopwatch { get; set; }
        public IDictionary<string, Substep> Substeps { get; set; }

        public Step(string name)
        {
            Name = name;
            Stopwatch = new Stopwatch();
            Substeps = new Dictionary<string, Substep>();
        }

        public StepInfo GetInfo()
        {
            return new StepInfo
                {
                    Name = Name,
                    Duration = Stopwatch.Elapsed,
                    Substeps = Substeps.Values.Select(x => x.GetInfo())
                };
        }
    }
}
