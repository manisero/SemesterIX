using System.Collections.Generic;

namespace GRM.Logic.GRMAlgorithm.Entities
{
    public class Node
    {
        public IList<Generator> Generators;

        public IList<int> TransactionIDs { get; set; }

        public int Support { get; set; }

        public bool IsDecisive { get; set; }

        public int DecisionID { get; set; }

        public IList<Node> Children { get; set; }

        public Node()
        {
            Children = new List<Node>();
        }
    }
}
