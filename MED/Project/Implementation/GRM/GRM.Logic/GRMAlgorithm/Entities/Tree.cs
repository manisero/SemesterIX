using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.GRMAlgorithm.Entities
{
    public class Tree
    {
        public Node Root { get; set; }

        public IDictionary<int, int> TransactionDecisions { get; set; }

        public IDictionary<int, IEnumerable<ItemID>> Generators { get; set; }
    }
}
