using System.Collections.Generic;

namespace GRM.Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies
{
    public class TIDSetsStorageStrategy : ITransactionIDsStorageStrategy
    {
        public IList<int> GetTreeRootTransactionIDs(IList<int> allTransactionIds)
        {
            throw new System.NotImplementedException();
        }

        public int GetTreeRootSupport(int allTransactionIdsCount)
        {
            throw new System.NotImplementedException();
        }

        public IList<int> GetChildTransactionIDs(IList<int> parentTransactionIds, IEnumerable<int> parentSiblingTransactionIds)
        {
            throw new System.NotImplementedException();
        }

        public int GetChildSupport(int parentSupport, IList<int> childTransactionIds)
        {
            throw new System.NotImplementedException();
        }
    }
}
