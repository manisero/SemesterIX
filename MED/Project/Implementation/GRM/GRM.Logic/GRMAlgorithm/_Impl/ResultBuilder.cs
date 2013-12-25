using System.Collections.Generic;
using System.Linq;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class ResultBuilder : IResultBuilder
    {
        private class GRMResultBuildState
        {
            public readonly IDictionary<int, IList<Generator>> DecisionGenerators = new Dictionary<int, IList<Generator>>();
        }

        private readonly GRMResultBuildState _buildState = new GRMResultBuildState();

        public void AppendDecisionGenerators(int decisionId, IList<Generator> generators)
        {
            if (!_buildState.DecisionGenerators.ContainsKey(decisionId))
            {
                _buildState.DecisionGenerators.Add(decisionId, generators);
            }
            else
            {
                var decisionGenerators = _buildState.DecisionGenerators[decisionId];

                // TODO: Check for subsets
                foreach (var generator in generators)
                {
                    decisionGenerators.Add(generator);
                }
            }
        }

        public GRMResult GetResult(IDictionary<string, int> decisionIds, IDictionary<Item, ItemID> itemIds)
        {
            var rules = new List<Rule>();

            foreach (var decisionGenerators in _buildState.DecisionGenerators)
            {
                var rule = new Rule
                {
                    Decision = decisionIds.Single(x => x.Value == decisionGenerators.Key).Key,
                    Generators = new List<IEnumerable<Item>>()
                };

                foreach (var generator in decisionGenerators.Value)
                {
                    var ruleGenerator = new List<Item>();

                    foreach (var itemId in generator)
                    {
                        ruleGenerator.Add(itemIds.Single(x => x.Value.Equals(itemId)).Key);
                    }

                    rule.Generators.Add(ruleGenerator);
                }
                
                rules.Add(rule);
            }

            return new GRMResult { Rules = rules };
        }
    }
}