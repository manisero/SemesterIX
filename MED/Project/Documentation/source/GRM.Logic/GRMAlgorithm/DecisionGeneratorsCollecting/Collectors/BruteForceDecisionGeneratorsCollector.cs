using System.Collections.Generic;
using System.Linq;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting.Collectors
{
    public class BruteForceDecisionGeneratorsCollector : DecisionGeneratorsCollectorBase
    {
        private readonly IDictionary<int, IList<Generator>> _decisionGenerators = new Dictionary<int, IList<Generator>>();

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
                    RemoveSupergenerators(generator, decisionGenerators);
                }

                foreach (var generator in generators)
                {
                    decisionGenerators.Add(generator);
                }
            }
        }

        private void RemoveSupergenerators(Generator subgenerator, IList<Generator> generators)
        {
            var supergenerators = new List<Generator>();

            foreach (var generator in generators)
            {
                if (subgenerator.All(generator.Contains))
                {
                    supergenerators.Add(generator);
                }
            }

            foreach (var supergenerator in supergenerators)
            {
                generators.Remove(supergenerator);
            }
        }

        public override IDictionary<int, IList<Generator>> GetDecisionsGenerators()
        {
            return _decisionGenerators;
        }
    }
}