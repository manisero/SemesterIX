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

        public void Execute(Node node, IDictionary<int, int> transactionDecisions, IDictionary<int, Generator> ruleGenerators, int minimalSupport)
        {
            for (int leftChildIndex = 0; leftChildIndex < node.Children.Count; leftChildIndex++)
            {
                for (int rightChildIndex = leftChildIndex + 1; rightChildIndex < node.Children.Count; rightChildIndex++)
                {
                    var leftChild = node.Children[leftChildIndex];
                    var rightChild = node.Children[rightChildIndex];

                    var property = _garmProperty.GetProperty(leftChild.TransactionIDs, rightChild.TransactionIDs);
                    _garmProperty.ApplyProperty(property, node, leftChild, rightChild, transactionDecisions, minimalSupport);
                }
            }

            // TODO: Implement
        }
    }
}
