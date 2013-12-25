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
                        Generators = new List<Generator> { new Generator(new ItemID { AttributeID = item.AttributeID, ValueID = item.ValueID }) },
                        IsDecisive = item.IsDecisive,
                        DecisionID = item.DecisionID,
                        TransactionIDs = item.TransactionIDs
                    };

                root.Children.Add(child);
            }

            return new Tree
                {
                    Root = root,
                    TransactionDecisions = transactionDecisions
                };
        }

        private Node CreateRoot(IDictionary<int, int> transactionDecisions)
        {
            var decisionId = transactionDecisions.Values.First();

            return new Node
                {
                    Generators = new List<Generator> { new Generator() },
                    TransactionIDs = transactionDecisions.Keys.ToList(),
                    DecisionID = decisionId,
                    IsDecisive = transactionDecisions.Values.All(x => x == decisionId)
                };
        }
    }
}