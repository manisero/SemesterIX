using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class GARMProcedure : IGARMProcedure
    {
        private readonly IResultBuilder _resultBuilder;
        private readonly IGARMPropertyProcedure _garmProperty;

        public GARMProcedure(IResultBuilder resultBuilder, IGARMPropertyProcedure garmProperty)
        {
            _resultBuilder = resultBuilder;
            _garmProperty = garmProperty;
        }

        public void Execute(Node node, IDictionary<int, int> transactionDecisions, int minimalSupport)
        {
            if (node.IsDecisive)
            {
                _resultBuilder.AppendDecisionGenerators(node.DecisionID, node.Generators);
                return;
            }

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

                UpdateChildGenerators(node.Generators, leftChild);

                Execute(leftChild, transactionDecisions, minimalSupport);
            }
        }

        private bool AreGeneratorsConflicted(IEnumerable<Generator> generators1, IEnumerable<Generator> generators2)
        {
            ProgressInfoContainer.CurrentProgressInfo.EnterSubstep("Checking for node generators conflicts");

            var result = false;

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
                                result = true;
                            }
                        }
                    }
                }
            }

            ProgressInfoContainer.CurrentProgressInfo.LeaveSubstep("Checking for node generators conflicts");

            return result;
        }

        private void UpdateChildGenerators(IEnumerable<Generator> parentGenerators, Node child)
        {
            ProgressInfoContainer.CurrentProgressInfo.EnterSubstep("Including parent node generators in child node generators");

            var newGenerators = new List<Generator>();

            foreach (var parentGenerator in parentGenerators)
            {
                foreach (var childGenerator in child.Generators)
                {
                    var newGenerator = new Generator(parentGenerator);
                    newGenerator.AddRange(childGenerator);

                    newGenerators.Add(newGenerator);
                }
            }

            child.Generators = newGenerators;

            ProgressInfoContainer.CurrentProgressInfo.LeaveSubstep("Including parent node generators in child node generators");
        }
    }
}
