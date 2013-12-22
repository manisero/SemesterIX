using System.Collections.Generic;

namespace GRM.Logic.GRMAlgorithm.Entities
{
    public class Tree
    {
        public Node Root { get; set; }

        public IDictionary<int, int> TransactionDecisions { get; set; }
    }
}
