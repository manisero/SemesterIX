using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm
{
    public interface IGARMPropertyProcedure
    {
        GARMPropertyType GetProperty(IEnumerable<int> leftChildTransactionIds, IEnumerable<int> rightChildTransactionIds);

        void AdjustProperty(GARMPropertyType property, Node parent, Node leftChild, Node rightChild);
    }
}