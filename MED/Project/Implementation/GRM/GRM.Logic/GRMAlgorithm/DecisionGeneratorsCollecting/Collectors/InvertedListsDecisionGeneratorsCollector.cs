using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;
using System.Linq;

namespace GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting.Collectors
{
    public class InvertedListsDecisionGeneratorsCollector : DecisionGeneratorsCollectorBase
    {
        private class InvertedList : Dictionary<ItemID, IList<Generator>>
        {
        }

        private readonly IDictionary<int, InvertedList> _decisionInvertedLists = new Dictionary<int, InvertedList>();

        protected override void AppendGenerators(int decisionId, IList<Generator> generators)
        {
            if (!_decisionInvertedLists.ContainsKey(decisionId))
            {
                _decisionInvertedLists.Add(decisionId, new InvertedList());
            }

            var invertedList = _decisionInvertedLists[decisionId];

            foreach (var generator in generators)
            {
                RemoveSupergenerators(generator, invertedList);
            }

            foreach (var generator in generators)
            {
                AppendGenerator(generator, invertedList);
            }
        }

        private void RemoveSupergenerators(Generator subgenerator, InvertedList invertedList)
        {
            if (!invertedList.ContainsKey(subgenerator[0]))
            {
                return;
            }

            IEnumerable<Generator> supergenerators = new List<Generator>(invertedList[subgenerator[0]]);

            for (int i = 1; i < subgenerator.Count; i++)
            {
                if (!invertedList.ContainsKey(subgenerator[i]))
                {
                    return;
                }

                supergenerators = supergenerators.Intersect(invertedList[subgenerator[i]]);
            }

            foreach (var supergenerator in supergenerators)
            {
                foreach (var generators in invertedList)
                {
                    generators.Value.Remove(supergenerator);
                }
            }
        }

        private void AppendGenerator(Generator generator, InvertedList invertedList)
        {
            foreach (var item in generator)
            {
                if (!invertedList.ContainsKey(item))
                {
                    invertedList.Add(item, new List<Generator> { generator });
                }
                else
                {
                    invertedList[item].Add(generator);
                }
            }
        }

        public override IDictionary<int, IList<Generator>> GetDecisionsGenerators()
        {
            var result = new Dictionary<int, IList<Generator>>();

            foreach (var decisionInvertedList in _decisionInvertedLists)
            {
                var generators = new List<Generator>();

                foreach (var invertedList in decisionInvertedList.Value)
                {
                    foreach (var generator in invertedList.Value)
                    {
                        if (!generators.Contains(generator))
                        {
                            generators.Add(generator);
                        }
                    }
                }

                result.Add(decisionInvertedList.Key, generators);
            }

            return result;
        }
    }
}
