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
            var decisionId = transactionDecisions.Values.First();

            root.DecisionID = decisionId;
            root.IsDecisive = transactionDecisions.Values.All(x => x == decisionId);
            root.DecisionsTransactionIDs = transactionDecisions.GroupBy(x => x.Value)
                                                              .ToDictionary(x => x.Key, x => (IList<int>)x.Select(pair => pair.Key).ToList());
        }

        public IList<int> GetFirstLevelChildTransactionIDs(IList<int> itemTransactionIds, IList<int> allTransactionIds)
        {
            return allTransactionIds.Except(itemTransactionIds).ToList();
        }

        public int GetFirstLevelChildSupport(int itemTransactionIdsCount)
        {
            return itemTransactionIdsCount;
        }

        public IDictionary<int, Node.DecisionTransactionIDs> GetFirstLevelChildDecisionsTransactionIDs(IList<int> itemTransactionIds, IDictionary<int, int> transactionDecisions)
        {
            var result = new Dictionary<int, IList<int>>();

            foreach (var transactionId in itemTransactionIds)
            {
                var decisionId = transactionDecisions[transactionId];

                if (!result.ContainsKey(decisionId))
                {
                    result.Add(decisionId, new List<int> { transactionId });
                }
                else
                {
                    result[decisionId].Add(transactionId);
                }
            }

            return result;
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