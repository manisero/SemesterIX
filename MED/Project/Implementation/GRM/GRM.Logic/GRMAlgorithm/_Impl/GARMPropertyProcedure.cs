using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage;
using GRM.Logic.ProgressTracking;
using GRM.Logic.Extensions;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class GARMPropertyProcedure : IGARMPropertyProcedure
    {
        private readonly int _determiningGARMPropertySubstepId = ProgressTrackerContainer.CurrentProgressTracker.RegisterSubstep("Determining GARM property");
        private readonly int _applyingGARMPropertySetsEqualSubstepId = ProgressTrackerContainer.CurrentProgressTracker.RegisterSubstep("Applying GARM property (sets equal)");
        private readonly int _applyingGARMPropertySetsDifferentSubstepId = ProgressTrackerContainer.CurrentProgressTracker.RegisterSubstep("Applying GARM property (sets different)");

        private readonly ITransactionIDsStorageStrategy _transactionIdsStorageStrategy;

        public GARMPropertyProcedure(ITransactionIDsStorageStrategy transactionIdsStorageStrategy)
        {
            _transactionIdsStorageStrategy = transactionIdsStorageStrategy;
        }

        public SetsRelationType GetProperty(Node leftChild, Node rightChild)
        {
            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(_determiningGARMPropertySubstepId);

            var result = _transactionIdsStorageStrategy.GetTransactionIDsRelation(leftChild, rightChild);

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(_determiningGARMPropertySubstepId);

            return result;
        }

        public void ApplyProperty(SetsRelationType property, Node parent, Node leftChild, Node rightChild, IDictionary<int,int> transactionDecisions, int minimalSupport)
        {
            if (property == SetsRelationType.Equality)
            {
                ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(_applyingGARMPropertySetsEqualSubstepId);

                foreach (var generator in rightChild.Generators)
                {
                    leftChild.Generators.Add(generator);
                }

                parent.Children.Remove(rightChild);

                ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(_applyingGARMPropertySetsEqualSubstepId);
            }
            else if (property == SetsRelationType.Difference)
            {
                ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(_applyingGARMPropertySetsDifferentSubstepId);

                var newChild = new Node();
                _transactionIdsStorageStrategy.SetChildTransactionIDsAndSupport(newChild, leftChild, rightChild);

                if (newChild.Support < minimalSupport)
                {
                    ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(_applyingGARMPropertySetsDifferentSubstepId);
                    return;
                }

                newChild.Generators = CopyGenerators(rightChild.Generators);
                _transactionIdsStorageStrategy.SetChildDecisiveness(newChild, leftChild.DecisionsTransactionIDs, rightChild.DecisionsTransactionIDs, transactionDecisions);

                leftChild.Children.Add(newChild);

                ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(_applyingGARMPropertySetsDifferentSubstepId);
            }
        }

        private IList<Generator> CopyGenerators(IEnumerable<Generator> generators)
        {
            var result = new List<Generator>();

            foreach (var generator in generators)
            {
                result.Add(new Generator(generator));
            }

            return result;
        }
    }
}