using System.Collections.Generic;
using System.Linq;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies
{
    public class TIDSetsStorageStrategy : ITransactionIDsStorageStrategy
    {
        public IList<int> GetTreeRootTransactionIDs(IList<int> allTransactionIds)
        {
            return allTransactionIds;
        }

        public int GetTreeRootSupport(int allTransactionIdsCount)
        {
            return allTransactionIdsCount;
        }

        public void SetTreeRootDecisiveness(IDictionary<int, int> transactionDecisions, Node root)
        {
            var decisionId = transactionDecisions.Values.First();

            root.DecisionID = decisionId;
            root.IsDecisive = transactionDecisions.Values.All(x => x == decisionId);
        }

        public IList<int> GetFirstLevelChildTransactionIDs(IList<int> itemTransactionIds, IList<int> allTransactionIds)
        {
            return itemTransactionIds;
        }

        public int GetFirstLevelChildSupport(int itemTransactionIdsCount)
        {
            return itemTransactionIdsCount;
        }

        public IList<int> GetChildTransactionIDs(IList<int> parentTransactionIds, IEnumerable<int> parentSiblingTransactionIds)
        {
            return parentTransactionIds.Intersect(parentSiblingTransactionIds).ToList();
        }

        public int GetChildSupport(int parentSupport, IList<int> childTransactionIds)
        {
            return childTransactionIds.Count;
        }
    }
}
