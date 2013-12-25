using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm
{
    public interface IGARMProcedure
    {
        void Execute(Node node, IDictionary<int, int> transactionDecisions, int minimalSupport);
    }
}