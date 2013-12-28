using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using System.Linq;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage;

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

            if (leftToRightSubsumption && rightToLeftSubsumption)
            {
                return GARMPropertyType.Equality;
            }
            else if (leftToRightSubsumption || rightToLeftSubsumption)
            {
                return GARMPropertyType.Subsumption;
            }
            else
            {
                return GARMPropertyType.Difference;
            }
        }

        private int? GetTransactionID(IList<int> transactionIds, int index)
        {
            return index < transactionIds.Count ? transactionIds[index] : (int?)null;
        }

        public void ApplyProperty(GARMPropertyType property, Node parent, Node leftChild, Node rightChild, IDictionary<int,int> transactionDecisions, int minimalSupport)
        {
            if (property == GARMPropertyType.Equality)
            {
                foreach (var generator in rightChild.Generators)
                {
                    leftChild.Generators.Add(generator);
                }

                parent.Children.Remove(rightChild);
            }
            else if (property == GARMPropertyType.Difference)
            {
                var newChildTransactionIds = _transactionIdsStorageStrategy.GetChildTransactionIDs(leftChild.TransactionIDs, rightChild.TransactionIDs);
                var newChildSupport = _transactionIdsStorageStrategy.GetChildSupport(leftChild.Support, newChildTransactionIds);

                if (newChildSupport < minimalSupport)
                {
                    return;
                }

                var decisionId = transactionDecisions[newChildTransactionIds[0]];
                
                var newChild = new Node
                    {
                        Generators = CopyGenerators(rightChild.Generators),
                        TransactionIDs = newChildTransactionIds,
                        Support = newChildSupport,
                        DecisionID = decisionId,
                        IsDecisive = newChildTransactionIds.Skip(1).All(x => transactionDecisions[x] == decisionId)
                    };

                leftChild.Children.Add(newChild);
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