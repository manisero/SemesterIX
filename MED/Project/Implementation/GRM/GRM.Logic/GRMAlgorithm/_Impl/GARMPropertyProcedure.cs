using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage;
using GRM.Logic.ProgressTracking;

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

        public GARMPropertyType GetProperty(IList<int> leftChildTransactionIds, IList<int> rightChildTransactionIds)
        {
            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(_determiningGARMPropertySubstepId);

            var leftToRightSubsumption = true;
            var rightToLeftSubsumption = true;

            var leftIndex = 0;
            var rightIndex = 0;

            while (leftIndex < leftChildTransactionIds.Count || rightIndex < rightChildTransactionIds.Count)
            {
                var leftChildTransactionId = GetTransactionID(leftChildTransactionIds, leftIndex);
                var rightChildTransactionId = GetTransactionID(rightChildTransactionIds, rightIndex);

                if (leftChildTransactionId == null || leftChildTransactionId > rightChildTransactionId)
                {
                    rightToLeftSubsumption = false;
                    rightIndex++;
                }
                else if (rightChildTransactionId == null || rightChildTransactionId > leftChildTransactionId)
                {
                    leftToRightSubsumption = false;
                    leftIndex++;
                }
                else
                {
                    leftIndex++;
                    rightIndex++;
                }

                if (!leftToRightSubsumption && !rightToLeftSubsumption)
                {
                    ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(_determiningGARMPropertySubstepId);

                    return GARMPropertyType.Difference;
                }
            }

            GARMPropertyType result;

            if (leftToRightSubsumption && rightToLeftSubsumption)
            {
                result = GARMPropertyType.Equality;
            }
            else
            {
                result = GARMPropertyType.Subsumption;
            }

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(_determiningGARMPropertySubstepId);

            return result;
        }

        private int? GetTransactionID(IList<int> transactionIds, int index)
        {
            return index < transactionIds.Count ? transactionIds[index] : (int?)null;
        }

        public void ApplyProperty(GARMPropertyType property, Node parent, Node leftChild, Node rightChild, IDictionary<int,int> transactionDecisions, int minimalSupport)
        {
            if (property == GARMPropertyType.Equality)
            {
                ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(_applyingGARMPropertySetsEqualSubstepId);

                foreach (var generator in rightChild.Generators)
                {
                    leftChild.Generators.Add(generator);
                }

                parent.Children.Remove(rightChild);

                ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(_applyingGARMPropertySetsEqualSubstepId);
            }
            else if (property == GARMPropertyType.Difference)
            {
                ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(_applyingGARMPropertySetsDifferentSubstepId);

                var newChildTransactionIds = _transactionIdsStorageStrategy.GetChildTransactionIDs(leftChild.TransactionIDs, rightChild.TransactionIDs);
                var newChildSupport = _transactionIdsStorageStrategy.GetChildSupport(leftChild.Support, newChildTransactionIds);

                if (newChildSupport < minimalSupport)
                {
                    ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(_applyingGARMPropertySetsDifferentSubstepId);
                    return;
                }
                
                var newChild = new Node
                    {
                        Generators = CopyGenerators(rightChild.Generators),
                        TransactionIDs = newChildTransactionIds,
                        Support = newChildSupport
                    };

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