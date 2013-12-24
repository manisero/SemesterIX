using System.Collections.Generic;
using System.Linq;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class ResultBuilder : IResultBuilder
    {
        public bool CanBuildResult(IDictionary<int, Generator> generators)
        {
            return generators.All(x => x.Value != null);
        }

        public GRMResult Build(IDictionary<int, Generator> generators, IDictionary<string, int> decisionIds, IDictionary<Item, ItemID> itemIds)
        {
            var rules = new List<Rule>();

            foreach (var generator in generators)
            {
                var items = new List<Item>();

                foreach (var itemId in generator.Value)
                {
                    items.Add(itemIds.Single(x => x.Value.Equals(itemId)).Key);
                }

                rules.Add(new Rule
                {
                    Items = items,
                    Decision = decisionIds.Single(x => x.Value == generator.Key).Key
                });
            }

            return new GRMResult { Rules = rules };
        }
    }
}