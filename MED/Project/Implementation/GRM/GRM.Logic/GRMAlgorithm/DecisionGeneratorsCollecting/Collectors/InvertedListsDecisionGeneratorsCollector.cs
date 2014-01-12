using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;

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
            // TODO: Implement
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
            throw new System.NotImplementedException();
        }
    }
}
