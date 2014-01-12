using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm.SupergeneratorsRemoval;
using GRM.Logic.ProgressTracking;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class DecisionGeneratorsCollector : IDecisionGeneratorsCollector
    {
        private class GRMResultBuildState
        {
            public readonly IDictionary<int, IList<Generator>> DecisionGenerators = new Dictionary<int, IList<Generator>>();
        }

        private readonly int _updatingDecisionGeneratorsSubstepId;

        private readonly ISupergeneratorsRemovalStrategy _supergeneratorsRemovalStrategy;

        private readonly GRMResultBuildState _buildState = new GRMResultBuildState();

        public DecisionGeneratorsCollector(ISupergeneratorsRemovalStrategy supergeneratorsRemovalStrategy)
        {
            _updatingDecisionGeneratorsSubstepId = ProgressTrackerContainer.CurrentProgressTracker.RegisterSubstep("Updating decision generators");

            _supergeneratorsRemovalStrategy = supergeneratorsRemovalStrategy;
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

        public IDictionary<int, IList<Generator>> GetDecisionsGenerators()
        {
            return _buildState.DecisionGenerators;
        }
    }
}