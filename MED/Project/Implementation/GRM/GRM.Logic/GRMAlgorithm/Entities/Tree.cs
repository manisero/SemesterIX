using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm._Impl;

namespace GRM.Logic.GRMAlgorithm.Entities
{
    public class Tree
    {
        public Node Root { get; set; }

        public IDictionary<int, int> TransactionDecisions { get; set; }

        public IDictionary<int, Generator> RuleGenerators { get; set; }
    }
}
