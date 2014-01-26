using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.Extensions;
using GRM.Logic.GRMAlgorithm.Entities;
using System.Linq;

namespace GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting.Collectors
{
    public class InvertedListsDecisionGeneratorsCollector : DecisionGeneratorsCollectorBase
    {
        private class DecisionGenerators
        {
            public SortedList<long, Generator> Generators = new SortedList<long, Generator>();

            public InvertedList InvertedList = new InvertedList();
        }
        
        private class InvertedList : Dictionary<ItemID, SortedList<long, bool>>
        {
        }

        private readonly IDictionary<int, DecisionGenerators> _decisionGenerators = new Dictionary<int, DecisionGenerators>();

        protected override void AppendGenerators(int decisionId, IList<Generator> generators)
        {
            if (!_decisionGenerators.ContainsKey(decisionId))
            {
                _decisionGenerators.Add(decisionId, new DecisionGenerators());
            }

            var decisionGenerators = _decisionGenerators[decisionId];

            foreach (var generator in generators)
            {
                RemoveSupergenerators(generator, decisionGenerators);
            }

            foreach (var generator in generators)
            {
                AppendGenerator(generator, decisionGenerators);
            }
        }

        private void RemoveSupergenerators(Generator subgenerator, DecisionGenerators decisionGenerators)
        {
            var invertedList = decisionGenerators.InvertedList;

            if (!invertedList.ContainsKey(subgenerator[0]))
            {
                return;
            }

            // Find supergenerators
            IList<long> supergenerators = new List<long>(invertedList[subgenerator[0]].Keys);

            for (int i = 1; i < subgenerator.Count; i++)
            {
                SortedList<long, bool> itemGenerators;

                if (!invertedList.TryGetValue(subgenerator[i], out itemGenerators))
                {
                    return;
                }

                supergenerators = supergenerators.SortedIntersect(itemGenerators.Keys);

                if (supergenerators.Count == 0)
                {
                    return;
                }
            }

            // Remove supergenerators
            foreach (var supergenerator in supergenerators)
            {
                decisionGenerators.Generators.Remove(supergenerator);

                foreach (var generators in invertedList)
                {
                    generators.Value.Remove(supergenerator);
                }
            }
        }

        private void AppendGenerator(Generator generator, DecisionGenerators decisionGenerators)
        {
            decisionGenerators.Generators.Add(generator.GetIdentifier(), generator);

            var invertedList = decisionGenerators.InvertedList;

            foreach (var item in generator)
            {
                if (!invertedList.ContainsKey(item))
                {
                    invertedList.Add(item, new SortedList<long, bool> { { generator.GetIdentifier(), false } });
                }
                else
                {
                    invertedList[item].Add(generator.GetIdentifier(), false );
                }
            }
        }

        public override IDictionary<int, IList<Generator>> GetDecisionsGenerators()
        {
            var result = new Dictionary<int, IList<Generator>>();

            foreach (var decisionInvertedList in _decisionGenerators)
            {
                result.Add(decisionInvertedList.Key, decisionInvertedList.Value.Generators.Values.ToList());
            }

            return result;
        }
    }
}
