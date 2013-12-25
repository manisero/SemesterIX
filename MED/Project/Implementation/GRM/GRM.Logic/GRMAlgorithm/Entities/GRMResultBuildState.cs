using System.Collections.Generic;

namespace GRM.Logic.GRMAlgorithm.Entities
{
    public class GRMResultBuildState
    {
        public IDictionary<int, IList<Generator>> DecisionGenerators { get; set; }

        public GRMResultBuildState()
        {
            DecisionGenerators = new Dictionary<int, IList<Generator>>();
        }
    }
}
