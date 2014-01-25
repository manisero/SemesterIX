using System.Collections.Generic;
using GRM.Logic.Extensions;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm
{
    public interface IGARMPropertyProcedure
    {
        SetsRelationType GetProperty(Node leftChild, Node rightChild);

        void ApplyProperty(SetsRelationType property, Node parent, Node leftChild, Node rightChild, IDictionary<int, int> transactionDecisions, int minimalSupport);
    }
}