using System.Collections.Generic;
using System.Linq;
using GRM.Logic.Extensions;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies
{
    public class DiffSetsStorageStrategy : ITransactionIDsStorageStrategy
    {
        public int[] GetTreeRootTransactionIDs(IList<int> allTransactionIds)
        {
            return new int[0];
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

        public int[] GetFirstLevelChildTransactionIDs(IList<int> itemTransactionIds, IList<int> allTransactionIds)
        {
            return allTransactionIds.SortedExcept(itemTransactionIds).ToArray();
        }

        public IDictionary<int, Node.DecisionTransactionIDs> GetFirstLevelChildDecisionsTransactionIDs(IList<int> itemTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> rootDecisionsTransactionIDs)
        {
            var result = new Dictionary<int, Node.DecisionTransactionIDs>();

            foreach (var rootDecisionTransactionIDs in rootDecisionsTransactionIDs)
            {
                var transactionIds = rootDecisionTransactionIDs.Value.TransactionIDs.SortedExcept(itemTransactionIds);
                var support = rootDecisionTransactionIDs.Value.Support - transactionIds.Count;

                if (support > 0)
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

        public int[] GetChildTransactionIDs(int[] parentTransactionIds, int[] parentSiblingTransactionIds)
        {
            return parentSiblingTransactionIds.SortedExcept(parentTransactionIds).ToArray();
        }

        public int GetChildSupport(int parentSupport, IList<int> childTransactionIds)
        {
            return parentSupport - childTransactionIds.Count;
        }

        public void SetChildDecisiveness(Node child, IDictionary<int, Node.DecisionTransactionIDs> parentDecisionsTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> parentSiblingDecisionsTransactionIds, IDictionary<int, int> transactionDecisions)
        {
            var decisionsTransactionIds = new Dictionary<int, Node.DecisionTransactionIDs>();

            foreach (var parentDecisionTransactionIds in parentDecisionsTransactionIds)
            {
                Node.DecisionTransactionIDs parentSiblingDecisionTransactionIds;

                if (!parentSiblingDecisionsTransactionIds.TryGetValue(parentDecisionTransactionIds.Key, out parentSiblingDecisionTransactionIds))
                {
                    continue;
                }

                var transactionIds = parentSiblingDecisionTransactionIds.TransactionIDs.SortedExcept(parentDecisionTransactionIds.Value.TransactionIDs);
                var support = parentDecisionTransactionIds.Value.Support - transactionIds.Count;

                if (support > 0)
                {
                    decisionsTransactionIds.Add(parentDecisionTransactionIds.Key, new Node.DecisionTransactionIDs { Support = support, TransactionIDs = transactionIds });
                }
            }

            child.DecisionsTransactionIDs = decisionsTransactionIds;
            child.DecisionID = decisionsTransactionIds.Keys.FirstOrDefault();
            child.IsDecisive = decisionsTransactionIds.Count == 1;
        }
    }
}