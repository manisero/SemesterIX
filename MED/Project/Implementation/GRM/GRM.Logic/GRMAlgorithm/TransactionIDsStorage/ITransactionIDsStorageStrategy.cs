using System.Collections.Generic;
using GRM.Logic.Extensions;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm.TransactionIDsStorage
{
    public interface ITransactionIDsStorageStrategy
    {
        int[] GetTreeRootTransactionIDs(IList<int> allTransactionIds);

        void SetTreeRootDecisiveness(Node root, IDictionary<int, int> transactionDecisions);

        int[] GetFirstLevelChildTransactionIDs(IList<int> itemTransactionIds, int[] allTransactionIds);

        IDictionary<int, Node.DecisionTransactionIDs> GetFirstLevelChildDecisionsTransactionIDs(IList<int> itemTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> rootDecisionsTransactionIDs);

        int GetFirstLevelChildSupport(int itemTransactionIdsCount);

        SetsRelationType GetTransactionIDsRelation(Node firstNode, Node secondNode);

        void SetChildTransactionIDsAndSupport(Node child, Node parent, Node parentSibling);

        void SetChildDecisiveness(Node child, IDictionary<int, Node.DecisionTransactionIDs> parentDecisionsTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> parentSiblingDecisionsTransactionIds, IDictionary<int, int> transactionDecisions);
    }
}
