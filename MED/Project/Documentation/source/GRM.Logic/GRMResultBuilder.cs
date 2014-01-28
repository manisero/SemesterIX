using System.Collections.Generic;
using System.Linq;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic
{
    public class GRMResultBuilder
    {
        public GRMResult GetResult(int attributesCount, int decisionAttributeIndex, IDictionary<int, string> attributeNames, IDictionary<string, int> decisionIds, IDictionary<Item, ItemID> itemIds, IDictionary<int, IList<Generator>> decisionsGenerators)
        {
            var result = new GRMResult
                {
                    AttributesCount = attributesCount,
                    DecisionAttributeIndex = decisionAttributeIndex,
                    AttributeNames = attributeNames
                };

            var rules = new List<Rule>();

            foreach (var decisionGenerators in decisionsGenerators)
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