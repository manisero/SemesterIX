using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm._Impl;

namespace GRM.Logic.GRMAlgorithm.Entities
{
    public class Node
    {
        public Generator Generator;

        public IEnumerable<int> TransactionIDs { get; set; }

        public bool IsDecisive { get; set; }

        public int DecisionID { get; set; }

        public IList<Node> Children { get; set; }

        public Node()
        {
            Children = new List<Node>();
        }
    }
}
