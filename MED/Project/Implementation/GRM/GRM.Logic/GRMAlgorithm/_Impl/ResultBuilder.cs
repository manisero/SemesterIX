using System.Collections.Generic;
using System.Linq;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class ResultBuilder : IResultBuilder
    {
        public bool CanBuildResult(IDictionary<int, IList<Generator>> generators)
        {
            return generators.All(x => x.Value != null && x.Value.Count != 0);
        }

        public GRMResult Build(IDictionary<int, IList<Generator>> generators, IDictionary<string, int> decisionIds, IDictionary<Item, ItemID> itemIds)
        {
            var rules = new List<Rule>();

            foreach (var decisionGenerators in generators)
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