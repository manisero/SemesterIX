using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class GARMPropertyProcedure : IGARMPropertyProcedure
    {
        public GARMPropertyType GetProperty(IEnumerable<int> leftChildTransactionIds, IEnumerable<int> rightChildTransactionIds)
        {
            throw new System.NotImplementedException();
        }

        public void AdjustProperty(GARMPropertyType property, Node parent, Node leftChild, Node rightChild)
        {
            // TODO: Implement
        }
    }
}