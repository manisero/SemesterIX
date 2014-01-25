using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;
using System.Linq;
using GRM.Logic.ProgressTracking;

namespace GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting.Collectors
{
    public class InvertedListsDecisionGeneratorsCollector : DecisionGeneratorsCollectorBase
    {
        private class DecisionGenerators
        {
            public IList<Generator> Generators = new List<Generator>();

            public InvertedList InvertedList = new InvertedList();
        }
        
        private class InvertedList : Dictionary<ItemID, IList<Generator>>
        {
        }

        private readonly IDictionary<int, DecisionGenerators> _decisionGenerators = new Dictionary<int, DecisionGenerators>();

        private int a = ProgressTrackerContainer.CurrentProgressTracker.RegisterSubstep("test");

        protected override void AppendGenerators(int decisionId, IList<Generator> generators)
        {
            if (!_decisionGenerators.ContainsKey(decisionId))
            {
                _decisionGenerators.Add(decisionId, new DecisionGenerators());
            }

            var decisionGenerators = _decisionGenerators[decisionId];

            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(a);

            foreach (var generator in generators)
            {
                RemoveSupergenerators(generator, decisionGenerators);
            }

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(a);
            
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

            var supergenerators = new List<Generator>(invertedList[subgenerator[0]]);

            for (int i = 1; i < subgenerator.Count; i++)
            {
                IList<Generator> itemGenerators;

                if (!invertedList.TryGetValue(subgenerator[i], out itemGenerators))
                {
                    return;
                }

                supergenerators = supergenerators.Intersect(itemGenerators).ToList();

                if (supergenerators.Count == 0)
                {
                    return;
                }
            }

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
            decisionGenerators.Generators.Add(generator);

            var invertedList = decisionGenerators.InvertedList;

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

            foreach (var decisionInvertedList in _decisionGenerators)
            {
                result.Add(decisionInvertedList.Key, decisionInvertedList.Value.Generators);
            }

            return result;
        }
    }
}
