using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.ProgressTracking;

namespace GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting
{
    public abstract class DecisionGeneratorsCollectorBase : IDecisionGeneratorsCollector
    {
        private readonly int _updatingDecisionGeneratorsSubstepId;   

        protected DecisionGeneratorsCollectorBase()
        {
            _updatingDecisionGeneratorsSubstepId = ProgressTrackerContainer.CurrentProgressTracker.RegisterSubstep("Updating decision generators");
        }

        public void AppendDecisionGenerators(int decisionId, IList<Generator> generators)
        {
            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(_updatingDecisionGeneratorsSubstepId);

            AppendGenerators(decisionId, generators);

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(_updatingDecisionGeneratorsSubstepId);
        }

        protected abstract void AppendGenerators(int decisionId, IList<Generator> generators);

        public abstract IDictionary<int, IList<Generator>> GetDecisionsGenerators();
    }
}