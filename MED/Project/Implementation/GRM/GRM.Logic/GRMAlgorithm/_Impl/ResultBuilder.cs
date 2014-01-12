using System.Collections.Generic;
using System.Linq;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm.SupergeneratorsRemoval;
using GRM.Logic.ProgressTracking;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class ResultBuilder : IResultBuilder
    {
        private class GRMResultBuildState
        {
            public readonly IDictionary<int, IList<Generator>> DecisionGenerators = new Dictionary<int, IList<Generator>>();
        }

        private readonly int _updatingDecisionGeneratorsSubstepId;

        private readonly ISupergeneratorsRemovalStrategy _supergeneratorsRemovalStrategy;

        private readonly GRMResultBuildState _buildState = new GRMResultBuildState();

        public ResultBuilder(ISupergeneratorsRemovalStrategy supergeneratorsRemovalStrategy)
        {
            _supergeneratorsRemovalStrategy = supergeneratorsRemovalStrategy;
            _updatingDecisionGeneratorsSubstepId = ProgressTrackerContainer.CurrentProgressTracker.RegisterSubstep("Updating decision generators");
        }

        public void AppendDecisionGenerators(int decisionId, IList<Generator> generators)
        {
            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(_updatingDecisionGeneratorsSubstepId);

            if (!_buildState.DecisionGenerators.ContainsKey(decisionId))
            {
                _buildState.DecisionGenerators.Add(decisionId, new List<Generator>(generators));
            }
            else
            {
                var decisionGenerators = _buildState.DecisionGenerators[decisionId];

                foreach (var generator in generators)
                {
                    _supergeneratorsRemovalStrategy.Apply(decisionGenerators, generator);
                }

                foreach (var generator in generators)
                {
                    decisionGenerators.Add(generator);
                }
            }

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(_updatingDecisionGeneratorsSubstepId);
        }

        public GRMResult GetResult(int attributesCount, int decisionAttributeIndex, IDictionary<int, string> attributeNames, IDictionary<string, int> decisionIds, IDictionary<Item, ItemID> itemIds)
        {
            var result = new GRMResult
            {
                AttributesCount = attributesCount,
                DecisionAttributeIndex = decisionAttributeIndex,
                AttributeNames = attributeNames
            };

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

            result.Rules = rules;

            return result;
        }
    }
}