using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.Extensions;
using GRM.Logic.GRMAlgorithm.Entities;
using System.Linq;
using GRM.Logic.ProgressTracking;

namespace GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting.Collectors
{
    public class InvertedListsDecisionGeneratorsCollector : DecisionGeneratorsCollectorBase
    {
        private class DecisionGenerators
        {
            public SortedList<long, Generator> Generators = new SortedList<long, Generator>();

            public InvertedList InvertedList = new InvertedList();
        }
        
        private class InvertedList : Dictionary<ItemID, SortedList<long, long>>
        {
        }

        private readonly IDictionary<int, DecisionGenerators> _decisionGenerators = new Dictionary<int, DecisionGenerators>();

        private int a = ProgressTrackerContainer.CurrentProgressTracker.RegisterSubstep("remove");
        private int b = ProgressTrackerContainer.CurrentProgressTracker.RegisterSubstep("append");

        protected override void AppendGenerators(int decisionId, IList<Generator> generators)
        {
            if (!_decisionGenerators.ContainsKey(decisionId))
            {
                _decisionGenerators.Add(decisionId, new DecisionGenerators());
            }

            foreach (var generator in generators) // TODO: Remove
            {
                generator.GetIdentifier();
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

            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(a);

            // Find supergenerators
            IList<long> supergenerators = new List<long>(invertedList[subgenerator[0]].Keys);

            for (int i = 1; i < subgenerator.Count; i++)
            {
                SortedList<long, long> itemGenerators;

                if (!invertedList.TryGetValue(subgenerator[i], out itemGenerators))
                {
                    ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(a);
                    return;
                }

                supergenerators = supergenerators.SortedIntersect(itemGenerators.Keys);

                if (supergenerators.Count == 0)
                {
                    ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(a);
                    return;
                }
            }

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(a);

            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(b);

            // Remove supergenerators
            foreach (var supergenerator in supergenerators)
            {
                decisionGenerators.Generators.Remove(supergenerator);

                foreach (var generators in invertedList)
                {
                    generators.Value.Remove(supergenerator);
                }
            }

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(b);
        }

        private void AppendGenerator(Generator generator, DecisionGenerators decisionGenerators)
        {
            decisionGenerators.Generators.Add(generator.GetIdentifier(), generator);

            var invertedList = decisionGenerators.InvertedList;

            foreach (var item in generator)
            {
                if (!invertedList.ContainsKey(item))
                {
                    invertedList.Add(item, new SortedList<long, long> { { generator.GetIdentifier(), generator.GetIdentifier() } });
                }
                else
                {
                    invertedList[item].Add(generator.GetIdentifier(), generator.GetIdentifier());
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
