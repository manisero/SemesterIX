using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.GRMAlgorithm.Entities
{
    public class Node
    {
        public IEnumerable<ItemID> Items;

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
