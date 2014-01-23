using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;
using System.Linq;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class TreeBuilder : ITreeBuilder
    {
        private readonly ITransactionIDsStorageStrategy _transactionIdsStorageStrategy;

        public TreeBuilder(ITransactionIDsStorageStrategy transactionIdsStorageStrategy)
        {
            _transactionIdsStorageStrategy = transactionIdsStorageStrategy;
        }

        public Node Build(IEnumerable<ItemInfo> frequentItems, IEnumerable<int> decisionIds, IDictionary<int, int> transactionDecisions)
        {
            var transactionIds = transactionDecisions.Keys.ToList();
            var root = CreateRoot(transactionIds, transactionDecisions);

            foreach (var item in frequentItems)
            {
                if (item.TransactionIDs.Count == transactionIds.Count)
                {
                    continue;
                }

                var child = new Node
                    {
                        Generators = new List<Generator> { new Generator(new ItemID { AttributeID = item.AttributeID, ValueID = item.ValueID }) },
                        TransactionIDs = _transactionIdsStorageStrategy.GetFirstLevelChildTransactionIDs(item.TransactionIDs, transactionIds),
                        DecisionsTransactionIDs = _transactionIdsStorageStrategy.GetFirstLevelChildDecisionsTransactionIDs(item.TransactionIDs, transactionDecisions),
                        IsDecisive = item.IsDecisive,
                        DecisionID = item.DecisionID,
                        Support = _transactionIdsStorageStrategy.GetFirstLevelChildSupport(item.TransactionIDs.Count)
                    };

                root.Children.Add(child);
            }

            return root;
        }

        private Node CreateRoot(IList<int> transactionIds, IDictionary<int, int> transactionDecisions)
        {
            var root = new Node
                {
                    Generators = new List<Generator> { new Generator() },
                    TransactionIDs = _transactionIdsStorageStrategy.GetTreeRootTransactionIDs(transactionIds),
                    Support = _transactionIdsStorageStrategy.GetTreeRootSupport(transactionIds.Count)
                };

            _transactionIdsStorageStrategy.SetTreeRootDecisiveness(root, transactionDecisions);

            return root;
        }
    }
}