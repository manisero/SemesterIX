using System.Collections.Generic;
using System.Linq;

namespace GRM.Logic.GRMAlgorithm.Entities
{
    public class Node
    {
        public class DecisionTransactionIDs
        {
            public int Support { get; set; }

            public int[] TransactionIDs { get; set; }
        }

        public IList<Generator> Generators;

        public int[] TransactionIDs { get; set; }

        public IDictionary<int, DecisionTransactionIDs> DecisionsTransactionIDs { get; set; }

        public int Support { get; set; }

        public bool IsDecisive { get; set; }

        public int DecisionID { get; set; }

        public IList<Node> Children { get; set; }

        public Node()
        {
            Children = new List<Node>();
        }

        public override string ToString()
        {
            var generators = Generators.Select(x => string.Join("; ", x.Select(itemId => itemId.ToString()).ToArray()));
            var formattedGenerators = generators.Select(x => string.Format("[{0}]", x));

            return string.Join(", ", formattedGenerators.ToArray());
        }
    }
}
