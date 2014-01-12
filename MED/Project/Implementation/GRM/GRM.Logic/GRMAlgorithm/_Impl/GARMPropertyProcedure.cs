using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage;
using GRM.Logic.ProgressTracking;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class GARMPropertyProcedure : IGARMPropertyProcedure
    {
        private readonly ITransactionIDsStorageStrategy _transactionIdsStorageStrategy;

        public GARMPropertyProcedure(ITransactionIDsStorageStrategy transactionIdsStorageStrategy)
        {
            _transactionIdsStorageStrategy = transactionIdsStorageStrategy;
        }

        public GARMPropertyType GetProperty(IList<int> leftChildTransactionIds, IList<int> rightChildTransactionIds)
        {
            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep("Determining GARM property");

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
            }

            GARMPropertyType result;

            if (leftToRightSubsumption && rightToLeftSubsumption)
            {
                result = GARMPropertyType.Equality;
            }
            else if (leftToRightSubsumption || rightToLeftSubsumption)
            {
                result = GARMPropertyType.Subsumption;
            }
            else
            {
                result = GARMPropertyType.Difference;
            }

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep("Determining GARM property");

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
                ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep("Applying GARM property (sets equal)");

                foreach (var generator in rightChild.Generators)
                {
                    leftChild.Generators.Add(generator);
                }

                parent.Children.Remove(rightChild);

                ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep("Applying GARM property (sets equal)");
            }
            else if (property == GARMPropertyType.Difference)
            {
                ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep("Applying GARM property (sets different)");

                var newChildTransactionIds = _transactionIdsStorageStrategy.GetChildTransactionIDs(leftChild.TransactionIDs, rightChild.TransactionIDs);
                var newChildSupport = _transactionIdsStorageStrategy.GetChildSupport(leftChild.Support, newChildTransactionIds);

                if (newChildSupport < minimalSupport)
                {
                    ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep("Applying GARM property (sets different)");
                    return;
                }
                
                var newChild = new Node
                    {
                        Generators = CopyGenerators(rightChild.Generators),
                        TransactionIDs = newChildTransactionIds,
                        Support = newChildSupport
                    };

                _transactionIdsStorageStrategy.SetChildDecisiveness(newChild, leftChild.DecisionTransactionIDs, transactionDecisions);

                leftChild.Children.Add(newChild);

                ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep("Applying GARM property (sets different)");
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