using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;
using System.Linq;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class TreeBuilder : ITreeBuilder
    {
        public Tree Build(IEnumerable<ItemInfo> frequentItems, IEnumerable<int> decisionIds, IDictionary<int, int> transactionDecisions)
        {
            var root = CreateRoot(transactionDecisions);
            var numberOfTransactions = transactionDecisions.Count;

            foreach (var item in frequentItems)
            {
                if (item.TransactionIDs.Count == numberOfTransactions)
                {
                    continue;
                }

                var child = new Node
                    {
                        Generator = new Generator { new ItemID { AttributeID = item.AttributeID, ValueID = item.ValueID } },
                        IsDecisive = item.IsDecisive,
                        DecisionID = item.DecisionID,
                        TransactionIDs = item.TransactionIDs
                    };

                root.Children.Add(child);
            }

            return new Tree
                {
                    Root = root,
                    TransactionDecisions = transactionDecisions,
                    RuleGenerators = GetRuleGenerators(decisionIds, root)
                };
        }

        private Node CreateRoot(IDictionary<int, int> transactionDecisions)
        {
            var decisionId = transactionDecisions.Values.First();

            return new Node
                {
                    Generator = new Generator(),
                    TransactionIDs = transactionDecisions.Keys,
                    DecisionID = decisionId,
                    IsDecisive = transactionDecisions.Values.All(x => x == decisionId)
                };
        }

        private IDictionary<int, Generator> GetRuleGenerators(IEnumerable<int> decisionIds, Node root)
        {
            var result = new Dictionary<int, Generator>();

            foreach (var decisionId in decisionIds)
            {
                result.Add(decisionId, null);
            }

            foreach (var child in root.Children)
            {
                if (child.IsDecisive)
                {
                    result[child.DecisionID] = child.Generator;
                }
            }

            if (root.IsDecisive)
            {
                result[root.DecisionID] = root.Generator;
            }

            return result;
        }
    }
}