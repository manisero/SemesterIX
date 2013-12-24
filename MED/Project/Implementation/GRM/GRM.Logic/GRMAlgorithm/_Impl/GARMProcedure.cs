using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class GARMProcedure
    {
        private readonly IGARMPropertyProcedure _garmProperty;

        public GARMProcedure(IGARMPropertyProcedure garmProperty)
        {
            _garmProperty = garmProperty;
        }

        public void Execute(Node node, int minimalSupport)
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
