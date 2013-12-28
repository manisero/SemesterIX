using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm.TransactionIDsStorage
{
    public interface ITransactionIDsStorageStrategy
    {
        IList<int> GetTreeRootTransactionIDs(IList<int> allTransactionIds);

        int GetTreeRootSupport(int allTransactionIdsCount);

        void SetTreeRootDecisiveness(IDictionary<int, int> transactionDecisions, Node root);

        IList<int> GetFirstLevelChildTransactionIDs(IList<int> itemTransactionIds, IList<int> allTransactionIds);

        int GetFirstLevelChildSupport(int itemTransactionIdsCount);
        
        IList<int> GetChildTransactionIDs(IList<int> parentTransactionIds, IEnumerable<int> parentSiblingTransactionIds);

        int GetChildSupport(int parentSupport, IList<int> childTransactionIds);
    }
}
