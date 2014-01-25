﻿using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm.TransactionIDsStorage
{
    public interface ITransactionIDsStorageStrategy
    {
        int[] GetTreeRootTransactionIDs(IList<int> allTransactionIds);

        int GetTreeRootSupport(int allTransactionIdsCount);

        void SetTreeRootDecisiveness(Node root, IDictionary<int, int> transactionDecisions);

        int[] GetFirstLevelChildTransactionIDs(IList<int> itemTransactionIds, IList<int> allTransactionIds);

        IDictionary<int, Node.DecisionTransactionIDs> GetFirstLevelChildDecisionsTransactionIDs(IList<int> itemTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> rootDecisionsTransactionIDs);

        int GetFirstLevelChildSupport(int itemTransactionIdsCount);

        int[] GetChildTransactionIDs(int[] parentTransactionIds, int[] parentSiblingTransactionIds);

        int GetChildSupport(int parentSupport, IList<int> childTransactionIds);

        void SetChildDecisiveness(Node child, IDictionary<int, Node.DecisionTransactionIDs> parentDecisionsTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> parentSiblingDecisionsTransactionIds, IDictionary<int, int> transactionDecisions);
    }
}
