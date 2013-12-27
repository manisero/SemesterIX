using System.Collections.Generic;
using System.Linq;

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