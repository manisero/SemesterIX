﻿using System.Collections.Generic;
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

        public void SetTreeRootDecisiveness(Node root, IDictionary<int, int> transactionDecisions)
        {
            var decisionId = transactionDecisions.Values.First();

            root.DecisionID = decisionId;
            root.IsDecisive = transactionDecisions.Values.All(x => x == decisionId);
        }

        public IList<int> GetFirstLevelChildTransactionIDs(IList<int> itemTransactionIds, IList<int> allTransactionIds)
        {
            return itemTransactionIds;
        }

        public IDictionary<int, Node.DecisionTransactionIDs> GetFirstLevelChildDecisionsTransactionIDs(IList<int> itemTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> rootDecisionsTransactionIDs)
        {
            return null;
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

        public void SetChildDecisiveness(Node child, IDictionary<int, Node.DecisionTransactionIDs> parentDecisionsTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> parentSiblingDecisionsTransactionIds, IDictionary<int, int> transactionDecisions)
        {
            var decisionId = transactionDecisions[child.TransactionIDs[0]];

            child.DecisionID = decisionId;
            child.IsDecisive = child.TransactionIDs.Skip(1).All(x => transactionDecisions[x] == decisionId);
        }
    }
}
