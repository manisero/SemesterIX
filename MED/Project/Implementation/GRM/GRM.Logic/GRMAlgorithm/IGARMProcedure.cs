using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm._Impl;

namespace GRM.Logic.GRMAlgorithm
{
    public interface IGARMProcedure
    {
        void Execute(Node node, IDictionary<int, int> transactionDecisions, IDictionary<int, Generator> ruleGenerators, int minimalSupport);
    }
}