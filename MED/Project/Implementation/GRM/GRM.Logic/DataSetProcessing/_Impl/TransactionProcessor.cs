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

            for (int i = 0; i < items.Length - 1; i++)
            {
                var itemId = GetItemID(buildState, i, items[i]);

                AppendItem(buildState, itemId, transactionId, decision);
            }
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

        private void AppendItem(DataSetRepresentationBuildState buildState, ItemID itemId, int transactionId, string decision)
        {
            if (!buildState.ItemInfos.ContainsKey(itemId))
            {
                buildState.ItemInfos.Add(itemId, new ItemInfo
                    {
                        TransactionIDs = new List<int> { transactionId },
                        IsDecisive = true,
                        Decision = decision
                    });
            }
            else
            {
                var itemInfo = buildState.ItemInfos[itemId];
                itemInfo.TransactionIDs.Add(transactionId);

                if (itemInfo.IsDecisive)
                {
                    itemInfo.IsDecisive = decision == itemInfo.Decision;
                }
            }
        }
    }
}
