using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm.SupergeneratorsRemoval;

namespace GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting.Collectors
{
    public class BruteForceDecisionGeneratorsCollector : DecisionGeneratorsCollectorBase
    {
        private readonly ISupergeneratorsRemovalStrategy _supergeneratorsRemovalStrategy;

        private readonly IDictionary<int, IList<Generator>> _decisionGenerators = new Dictionary<int, IList<Generator>>();

        public BruteForceDecisionGeneratorsCollector(ISupergeneratorsRemovalStrategy supergeneratorsRemovalStrategy) 
        {
            _supergeneratorsRemovalStrategy = supergeneratorsRemovalStrategy;
        }

        protected override void AppendGenerators(int decisionId, IList<Generator> generators)
        {
            if (!_decisionGenerators.ContainsKey(decisionId))
            {
                _decisionGenerators.Add(decisionId, new List<Generator>(generators));
            }
            else
            {
                var decisionGenerators = _decisionGenerators[decisionId];

                foreach (var generator in generators)
                {
                    _supergeneratorsRemovalStrategy.Apply(decisionGenerators, generator);
                }

                foreach (var generator in generators)
                {
                    decisionGenerators.Add(generator);
                }
            }
        }

        public override IDictionary<int, IList<Generator>> GetDecisionsGenerators()
        {
            return _decisionGenerators;
        }
    }
}