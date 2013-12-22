using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;
using System.Linq;

namespace GRM.Logic.GRMAlgorithm._Impl
{
    public class TreeBuilder : ITreeBuilder
    {
        public Tree Build(IEnumerable<ItemInfo> frequentItems, IDictionary<int, int> transactionDecisions)
        {
            var root = CreateRoot(transactionDecisions);

            foreach (var item in frequentItems)
            {
                var child = new Node
                    {
                        Items = new List<ItemID> { new ItemID { AttributeID = item.AttributeID, ValueID = item.ValueID } },
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
                    Items = new ItemID[0],
                    TransactionIDs = transactionDecisions.Keys,
                    DecisionID = decisionId,
                    IsDecisive = transactionDecisions.Values.All(x => x == decisionId)
                };
        }
    }
}