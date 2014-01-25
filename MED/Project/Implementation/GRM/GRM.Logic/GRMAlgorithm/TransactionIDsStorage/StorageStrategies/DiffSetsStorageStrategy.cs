using System.Collections.Generic;
using System.Linq;
using GRM.Logic.Extensions;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies
{
    public class DiffSetsStorageStrategy : ITransactionIDsStorageStrategy
    {
        public int[] GetTreeRootTransactionIDs(IList<int> allTransactionIds)
        {
            return new int[0];
        }

        public int GetTreeRootSupport(int allTransactionIdsCount)
        {
            return allTransactionIdsCount;
        }

        public void SetTreeRootDecisiveness(Node root, IDictionary<int, int> transactionDecisions)
        {
            root.DecisionsTransactionIDs = transactionDecisions.GroupBy(x => x.Value)
                                                               .ToDictionary(x => x.Key,
                                                                             x => new Node.DecisionTransactionIDs
                                                                                 {
                                                                                     Support = x.Count(),
                                                                                     TransactionIDs = x.Select(pair => pair.Key).ToArray()
                                                                                 });

            var decisionId = transactionDecisions.Values.First();

            root.DecisionID = decisionId;
            root.IsDecisive = root.DecisionsTransactionIDs.Count == 1;
        }

        public int[] GetFirstLevelChildTransactionIDs(IList<int> itemTransactionIds, int[] allTransactionIds)
        {
            return allTransactionIds.SortedExcept(itemTransactionIds.ToArray());
        }

        public IDictionary<int, Node.DecisionTransactionIDs> GetFirstLevelChildDecisionsTransactionIDs(IList<int> itemTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> rootDecisionsTransactionIDs)
        {
            var result = new Dictionary<int, Node.DecisionTransactionIDs>();

            foreach (var rootDecisionTransactionIDs in rootDecisionsTransactionIDs)
            {
                var transactionIds = rootDecisionTransactionIDs.Value.TransactionIDs.SortedExcept(itemTransactionIds.ToArray());
                var support = rootDecisionTransactionIDs.Value.Support - transactionIds.Length;

                if (support > 0)
                {
                    result.Add(rootDecisionTransactionIDs.Key, new Node.DecisionTransactionIDs { Support = support, TransactionIDs = transactionIds });
                }
            }

            return result;
        }

        public int GetFirstLevelChildSupport(int itemTransactionIdsCount)
        {
            return itemTransactionIdsCount;
        }

        public SetsRelationType GetTransactionIDsRelation(Node firstNode, Node secondNode)
        {
            var firstSubsumesSecond = true;
            var secondSubsumesFirst = true;

            foreach (var firstNodeDecisionTransactionIds in firstNode.DecisionsTransactionIDs)
            {
                Node.DecisionTransactionIDs secondNodeDecisionTransactionIds;

                if (secondNode.DecisionsTransactionIDs.TryGetValue(firstNodeDecisionTransactionIds.Key, out secondNodeDecisionTransactionIds))
                {
                    var relation = firstNodeDecisionTransactionIds.Value.TransactionIDs.SortedGetSetsRelation(secondNodeDecisionTransactionIds.TransactionIDs);

                    if (relation == SetsRelationType.Difference)
                    {
                        return SetsRelationType.Difference;
                    }
                    else if (relation == SetsRelationType.FirstSubsumesSecond)
                    {
                        secondSubsumesFirst = false;
                    }
                    else if (relation == SetsRelationType.SecondSubsumesFirst)
                    {
                        firstSubsumesSecond = false;
                    }
                }
                else
                {
                    secondSubsumesFirst = false;
                }

                if (!firstSubsumesSecond && !secondSubsumesFirst)
                {
                    return SetsRelationType.Difference;
                }
            }

            foreach (var secondNodeDecisionTransactionIds in secondNode.DecisionsTransactionIDs)
            {
                if (!firstNode.DecisionsTransactionIDs.ContainsKey(secondNodeDecisionTransactionIds.Key))
                {
                    firstSubsumesSecond = false;
                }
            }

            if (secondSubsumesFirst && firstSubsumesSecond)
            {
                return SetsRelationType.Equality;
            }
            else if (firstSubsumesSecond)
            {
                return SetsRelationType.FirstSubsumesSecond;
            }
            else
            {
                return SetsRelationType.SecondSubsumesFirst;
            }
        }

        public void SetChildTransactionIDsAndSupport(Node child, Node parent, Node parentSibling)
        {
            var decisionsTransactionIds = new Dictionary<int, Node.DecisionTransactionIDs>();
            child.Support = 0;

            foreach (var parentDecisionTransactionIds in parent.DecisionsTransactionIDs)
            {
                Node.DecisionTransactionIDs parentSiblingDecisionTransactionIds;

                if (!parentSibling.DecisionsTransactionIDs.TryGetValue(parentDecisionTransactionIds.Key, out parentSiblingDecisionTransactionIds))
                {
                    continue;
                }

                var decisionTransactionIds = parentSiblingDecisionTransactionIds.TransactionIDs.SortedExcept(parentDecisionTransactionIds.Value.TransactionIDs);
                var support = parentDecisionTransactionIds.Value.Support - decisionTransactionIds.Length;

                if (support > 0)
                {
                    decisionsTransactionIds.Add(parentDecisionTransactionIds.Key, new Node.DecisionTransactionIDs { Support = support, TransactionIDs = decisionTransactionIds });

                    child.Support += support;
                }
            }

            child.DecisionsTransactionIDs = decisionsTransactionIds;
        }

        public void SetChildDecisiveness(Node child, IDictionary<int, Node.DecisionTransactionIDs> parentDecisionsTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> parentSiblingDecisionsTransactionIds, IDictionary<int, int> transactionDecisions)
        {
            child.DecisionID = child.DecisionsTransactionIDs.Keys.FirstOrDefault();
            child.IsDecisive = child.DecisionsTransactionIDs.Count == 1;
        }
    }
}