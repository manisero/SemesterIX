using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using System.Linq;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class GARMPropertyProcedure : IGARMPropertyProcedure
    {
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

                if (leftChildTransactionId > rightChildTransactionId)
                {
                    rightToLeftSubsumption = false;
                    rightIndex++;
                }
                else if (rightChildTransactionId > leftChildTransactionId)
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

        public void ApplyProperty(GARMPropertyType property, Node parent, Node leftChild, Node rightChild, IDictionary<int,int> transactionDecisions, int minimalSupport)
        {
            if (property == GARMPropertyType.Equality)
            {
                parent.Children.Remove(rightChild);
            }
            else if (property == GARMPropertyType.Difference)
            {
                var transactionIds = leftChild.TransactionIDs.Intersect(rightChild.TransactionIDs).ToList();

                if (transactionIds.Count <= minimalSupport)
                {
                    return;
                }

                var decisionId = transactionDecisions[transactionIds[0]];
                
                var newChild = new Node
                    {
                        Generators = MergeGenerators(leftChild.Generators, rightChild.Generators),
                        TransactionIDs = transactionIds,
                        DecisionID = decisionId,
                        IsDecisive = transactionIds.Skip(1).All(x => transactionDecisions[x] == decisionId)
                    };

                leftChild.Children.Add(newChild);
            }
        }

        private int? GetTransactionID(IList<int> transactionIds, int index)
        {
            return index < transactionIds.Count ? transactionIds[index] : (int?)null;
        }

        private IList<Generator> MergeGenerators(IEnumerable<Generator> leftChildGenerators, IEnumerable<Generator> rightChildGenerators)
        {
            var result = new List<Generator>();

            foreach (var generator in leftChildGenerators)
            {
                result.Add(new Generator(generator));
            }

            foreach (var generator in rightChildGenerators)
            {
                result.Add(new Generator(generator));
            }

            return result;
        }
    }
}