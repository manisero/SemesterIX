using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.DataSetProcessing._Impl
{
    public class TransactionProcessor : ITransactionProcessor
    {
        public void AppendTransaction(int transactionId, string transaction, DataSetRepresentationBuildState buildState)
        {
            var items = transaction.Split(',');
            var decision = items[items.Length - 1];

            var decisionId = GetDecisionID(buildState, decision);
            buildState.TransactionDecisions.Add(transactionId, decisionId);

            for (int i = 0; i < items.Length - 1; i++)
            {
                var itemId = GetItemID(buildState, i, items[i]);

                AppendItem(buildState, itemId, transactionId, decisionId);
            }
        }

        private int GetDecisionID(DataSetRepresentationBuildState buildState, string decision)
        {
            int decisionId;

            if (!buildState.DecisionIDs.ContainsKey(decision))
            {
                decisionId = buildState.DecisionMappingCounter;

                buildState.DecisionIDs.Add(decision, decisionId);
                buildState.DecisionMappingCounter++;
            }
            else
            {
                decisionId = buildState.DecisionIDs[decision];
            }

            return decisionId;
        }

        private ItemID GetItemID(DataSetRepresentationBuildState buildState, int attributeId, string attributeValue)
        {
            var item = new Item
                {
                    AttributeID = attributeId,
                    Value = attributeValue
                };

            ItemID itemId;

            if (!buildState.ItemIDs.ContainsKey(item))
            {
                itemId = new ItemID
                    {
                        AttributeID = attributeId,
                        ValueID = buildState.ItemValueMappingCounter
                    };

                buildState.ItemIDs.Add(item, itemId);
                buildState.ItemValueMappingCounter++;
            }
            else
            {
                itemId = buildState.ItemIDs[item];
            }

            return itemId;
        }

        private void AppendItem(DataSetRepresentationBuildState buildState, ItemID itemId, int transactionId, int decisionId)
        {
            if (!buildState.ItemInfos.ContainsKey(itemId))
            {
                buildState.ItemInfos.Add(itemId, new ItemInfo
                    {
                        AttributeID = itemId.AttributeID,
                        ValueID = itemId.ValueID,
                        TransactionIDs = new List<int> { transactionId },
                        IsDecisive = true,
                        DecisionID = decisionId
                    });
            }
            else
            {
                var itemInfo = buildState.ItemInfos[itemId];
                itemInfo.TransactionIDs.Add(transactionId);

                if (itemInfo.IsDecisive)
                {
                    itemInfo.IsDecisive = decisionId == itemInfo.DecisionID;
                }
            }
        }
    }
}
