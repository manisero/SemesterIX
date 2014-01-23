using System.Collections.Generic;
using System.Linq;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies
{
    public class DiffSetsStorageStrategy : ITransactionIDsStorageStrategy
    {
        public IList<int> GetTreeRootTransactionIDs(IList<int> allTransactionIds)
        {
            return new List<int>();
        }

        public int GetTreeRootSupport(int allTransactionIdsCount)
        {
            return allTransactionIdsCount;
        }

        public void SetTreeRootDecisiveness(Node root, IDictionary<int, int> transactionDecisions)
        {
            root.DecisionsTransactionIDs = transactionDecisions.GroupBy(x => x.Value)
                                                               .ToDictionary(x => x.Key,
                                                                             x => new Node.DecisionTransactionIDs
                                                                                 {
                                                                                     Support = x.Count(),
                                                                                     TransactionIDs = (IList<int>)x.Select(pair => pair.Key).ToList()
                                                                                 });

            var decisionId = transactionDecisions.Values.First();

            root.DecisionID = decisionId;
            root.IsDecisive = root.DecisionsTransactionIDs.Count == 1;
        }

        public IList<int> GetFirstLevelChildTransactionIDs(IList<int> itemTransactionIds, IList<int> allTransactionIds)
        {
            return allTransactionIds.Except(itemTransactionIds).ToList();
        }

        public IDictionary<int, Node.DecisionTransactionIDs> GetFirstLevelChildDecisionsTransactionIDs(IList<int> itemTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> rootDecisionsTransactionIDs)
        {
            var result = new Dictionary<int, Node.DecisionTransactionIDs>();

            foreach (var rootDecisionTransactionIDs in rootDecisionsTransactionIDs)
            {
                var support = rootDecisionTransactionIDs.Value.Support;
                var transactionIds = new List<int>();

                foreach (var transactionId in rootDecisionTransactionIDs.Value.TransactionIDs)
                {
                    if (!itemTransactionIds.Contains(transactionId))
                    {
                        support--;
                        transactionIds.Add(transactionId);
                    }
                }

                if (support != 0)
                {
                    result.Add(rootDecisionTransactionIDs.Key, new Node.DecisionTransactionIDs { Support = support, TransactionIDs = transactionIds });
                }
            }

            return result;
        }

        public int GetFirstLevelChildSupport(int itemTransactionIdsCount)
        {
            return itemTransactionIdsCount;
        }

        public IList<int> GetChildTransactionIDs(IList<int> parentTransactionIds, IEnumerable<int> parentSiblingTransactionIds)
        {
            return parentSiblingTransactionIds.Except(parentTransactionIds).ToList();
        }

        public int GetChildSupport(int parentSupport, IList<int> childTransactionIds)
        {
            return parentSupport - childTransactionIds.Count;
        }

        public void SetChildDecisiveness(Node child, IDictionary<int, Node.DecisionTransactionIDs> parentDecisionsTransactionIds, IDictionary<int, int> transactionDecisions)
        {
            var decisionTransactionIds = new Dictionary<int, IList<int>>();

            foreach (var parentTransactionIds in parentDecisionsTransactionIds)
            {
                var transactionIds = parentTransactionIds.Value.Except(child.TransactionIDs).ToList();

                if (transactionIds.Count != 0)
                {
                    decisionTransactionIds.Add(parentTransactionIds.Key, transactionIds);
                }
            }

            child.DecisionsTransactionIDs = decisionTransactionIds;
            child.DecisionID = decisionTransactionIds.Keys.First();
            child.IsDecisive = decisionTransactionIds.Count == 1;
        }
    }
}