using System.Collections.Generic;
using System.Linq;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.Extensions;

namespace GRM.Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies
{
    public class TIDSetsStorageStrategy : ITransactionIDsStorageStrategy
    {
        public int[] GetTreeRootTransactionIDs(IList<int> allTransactionIds)
        {
            return allTransactionIds.ToArray();
        }

        public void SetTreeRootDecisiveness(Node root, IDictionary<int, int> transactionDecisions)
        {
            var decisionId = transactionDecisions.Values.First();

            root.DecisionID = decisionId;
            root.IsDecisive = transactionDecisions.Values.All(x => x == decisionId);
        }

        public int[] GetFirstLevelChildTransactionIDs(IList<int> itemTransactionIds, int[] allTransactionIds)
        {
            return itemTransactionIds.ToArray();
        }

        public IDictionary<int, Node.DecisionTransactionIDs> GetFirstLevelChildDecisionsTransactionIDs(IList<int> itemTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> rootDecisionsTransactionIDs)
        {
            return null;
        }

        public int GetFirstLevelChildSupport(int itemTransactionIdsCount)
        {
            return itemTransactionIdsCount;
        }

        public SetsRelationType GetTransactionIDsRelation(Node firstNode, Node secondNode)
        {
            return firstNode.TransactionIDs.SortedGetSetsRelation(secondNode.TransactionIDs);
        }

        public void SetChildTransactionIDsAndSupport(Node child, Node parent, Node parentSibling)
        {
            child.TransactionIDs = parent.TransactionIDs.SortedIntersect(parentSibling.TransactionIDs);
            child.Support = child.TransactionIDs.Length;
        }

        public void SetChildDecisiveness(Node child, IDictionary<int, Node.DecisionTransactionIDs> parentDecisionsTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> parentSiblingDecisionsTransactionIds, IDictionary<int, int> transactionDecisions)
        {
            var decisionId = transactionDecisions[child.TransactionIDs[0]];

            child.DecisionID = decisionId;
            child.IsDecisive = child.TransactionIDs.All(x => transactionDecisions[x] == decisionId);
        }
    }
}
