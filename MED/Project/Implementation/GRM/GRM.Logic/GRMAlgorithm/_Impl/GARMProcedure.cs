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
                    _garmProperty.Execute(node, node.Children[leftChildIndex], node.Children[rightChildIndex]);
                }
            }

            // TODO: Implement
        }
    }
}
