using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class GARMProcedure : IGARMProcedure
    {
        private readonly IGARMPropertyProcedure _garmProperty;

        public GARMProcedure(IGARMPropertyProcedure garmProperty)
        {
            _garmProperty = garmProperty;
        }

        public void Execute(Node node, IDictionary<int, int> transactionDecisions, IDictionary<int, IList<Generator>> ruleGenerators, int minimalSupport)
        {
            for (int leftChildIndex = 0; leftChildIndex < node.Children.Count; leftChildIndex++)
            {
                var leftChild = node.Children[leftChildIndex];

                for (int rightChildIndex = leftChildIndex + 1; rightChildIndex < node.Children.Count; rightChildIndex++)
                {
                    var rightChild = node.Children[rightChildIndex];

                    if (AreGeneratorsConflicted(leftChild.Generators, rightChild.Generators))
                    {
                        continue;
                    }

                    var property = _garmProperty.GetProperty(leftChild.TransactionIDs, rightChild.TransactionIDs);
                    _garmProperty.ApplyProperty(property, node, leftChild, rightChild, transactionDecisions, minimalSupport);
                }

                foreach (var parentGenerator in node.Generators)
                {
                    foreach (var itemId in parentGenerator)
                    {
                        foreach (var childGenerator in leftChild.Generators)
                        {
                            childGenerator.Add(itemId);
                        }
                    }
                }

                // TODO: Update ruleGenerators

                Execute(leftChild, transactionDecisions, ruleGenerators, minimalSupport);
            }
        }

        private bool AreGeneratorsConflicted(IEnumerable<Generator> generators1, IEnumerable<Generator> generators2)
        {
            foreach (var generator1 in generators1)
            {
                foreach (var itemId1 in generator1)
                {
                    foreach (var generator2 in generators2)
                    {
                        foreach (var itemId2 in generator2)
                        {
                            if (itemId1.AttributeID == itemId2.AttributeID && itemId1.ValueID != itemId2.ValueID)
                            {
                                return true;
                            }
                        }
                    }
                }
            }

            return false;
        }
    }
}
