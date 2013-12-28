using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm.TransactionIDsStorage
{
    public interface ITransactionIDsStorageStrategy
    {
        IList<int> GetTreeRootTransactionIDs(IList<int> allTransactionIds);

        int GetTreeRootSupport(int allTransactionIdsCount);

        void SetTreeRootDecisiveness(Node root, IDictionary<int, int> transactionDecisions);

        IList<int> GetFirstLevelChildTransactionIDs(IList<int> itemTransactionIds, IList<int> allTransactionIds);

        int GetFirstLevelChildSupport(int itemTransactionIdsCount);

        IDictionary<int, IList<int>> GetFirstLevelChildDecisionTransactionIDs(IList<int> itemTransactionIds, IDictionary<int, int> transactionDecisions);
        
        IList<int> GetChildTransactionIDs(IList<int> parentTransactionIds, IEnumerable<int> parentSiblingTransactionIds);

        int GetChildSupport(int parentSupport, IList<int> childTransactionIds);

        void SetChildDecisiveness(Node child, IDictionary<int, IList<int>> parentDecisionTransactionIds, IDictionary<int, int> transactionDecisions);
    }
}
