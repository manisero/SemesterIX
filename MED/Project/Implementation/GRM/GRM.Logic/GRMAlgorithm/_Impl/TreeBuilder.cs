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
            var root = CreateRoot(transactionDecisions);
            var numberOfTransactions = transactionDecisions.Count;

            foreach (var item in frequentItems)
            {
                if (item.TransactionIDs.Count == numberOfTransactions)
                {
                    continue;
                }

                var child = new Node
                    {
                        Generators = new List<Generator> { new Generator(new ItemID { AttributeID = item.AttributeID, ValueID = item.ValueID }) },
                        IsDecisive = item.IsDecisive,
                        DecisionID = item.DecisionID,
                        TransactionIDs = item.TransactionIDs,
                        Support = item.TransactionIDs.Count
                    };

                root.Children.Add(child);
            }

            return root;
        }

        private Node CreateRoot(IDictionary<int, int> transactionDecisions)
        {
            var decisionId = transactionDecisions.Values.First();

            return new Node
                {
                    Generators = new List<Generator> { new Generator() },
                    TransactionIDs = _transactionIdsStorageStrategy.GetTreeRootTransactionIDs(transactionDecisions.Keys.ToList()),
                    Support = _transactionIdsStorageStrategy.GetTreeRootSupport(transactionDecisions.Keys.Count),
                    DecisionID = decisionId,
                    IsDecisive = transactionDecisions.Values.All(x => x == decisionId)
                };
        }
    }
}