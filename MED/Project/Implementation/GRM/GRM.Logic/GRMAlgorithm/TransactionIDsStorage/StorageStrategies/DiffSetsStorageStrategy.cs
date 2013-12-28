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

        public void SetTreeRootDecisiveness(IDictionary<int, int> transactionDecisions, Node root)
        {
            var decisionId = transactionDecisions.Values.First();

            root.DecisionID = decisionId;
            root.IsDecisive = transactionDecisions.Values.All(x => x == decisionId);
            root.DecisionTransactionIDs = transactionDecisions.GroupBy(x => x.Value)
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

        public IList<int> GetChildTransactionIDs(IList<int> parentTransactionIds, IEnumerable<int> parentSiblingTransactionIds)
        {
            return parentSiblingTransactionIds.Except(parentTransactionIds).ToList();
        }

        public int GetChildSupport(int parentSupport, IList<int> childTransactionIds)
        {
            return parentSupport - childTransactionIds.Count;
        }
    }
}