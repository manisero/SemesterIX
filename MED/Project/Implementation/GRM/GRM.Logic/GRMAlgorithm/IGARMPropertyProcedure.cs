using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm
{
    public interface IGARMPropertyProcedure
    {
        GARMPropertyType GetProperty(IList<int> leftChildTransactionIds, IList<int> rightChildTransactionIds);

        void ApplyProperty(GARMPropertyType property, Node parent, Node leftChild, Node rightChild);
    }
}