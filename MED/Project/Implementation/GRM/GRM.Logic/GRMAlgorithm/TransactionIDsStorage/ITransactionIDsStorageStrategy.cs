using System.Collections.Generic;

namespace GRM.Logic.GRMAlgorithm.TransactionIDsStorage
{
    public interface ITransactionIDsStorageStrategy
    {
        IList<int> GetTreeRootTransactionIDs(IList<int> allTransactionIds);

        int GetTreeRootSupport(int allTransactionIdsCount);

        IList<int> GetChildTransactionIDs(IList<int> parentTransactionIds, IEnumerable<int> parentSiblingTransactionIds);

        int GetChildSupport(int parentSupport, IList<int> childTransactionIds);
    }
}
