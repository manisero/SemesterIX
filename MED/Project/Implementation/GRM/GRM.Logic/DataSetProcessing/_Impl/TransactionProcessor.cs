using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.ProgressTracking;

namespace GRM.Logic.DataSetProcessing._Impl
{
    public class TransactionProcessor : ITransactionProcessor
    {
        public void AppendTransaction(int transactionId, string transaction, int decisionAttributeIndex, DataSetRepresentationBuildState buildState)
        {
            var items = transaction.Split(',');
            var decision = items[decisionAttributeIndex];

            var decisionId = GetDecisionID(buildState, decision);
            buildState.TransactionDecisions.Add(transactionId, decisionId);

            for (int i = 0; i < items.Length; i++)
            {
                if (i == decisionAttributeIndex)
                {
                    continue;
                }

                var item = items[i];

                if (item.Trim().Length == 0)
                {
                    continue;
                }

                var itemId = GetItemID(buildState, i, item);

                AppendItem(buildState, itemId, transactionId, decisionId);
            }
        }

        private int GetDecisionID(DataSetRepresentationBuildState buildState, string decision)
        {
            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep("Building decision -> decision id dictionary");

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

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep("Building decision -> decision id dictionary");

            return decisionId;
        }

        private ItemID GetItemID(DataSetRepresentationBuildState buildState, int attributeId, string attributeValue)
        {
            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep("Building item -> item id dictionary");

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

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep("Building item -> item id dictionary");

            return itemId;
        }

        private void AppendItem(DataSetRepresentationBuildState buildState, ItemID itemId, int transactionId, int decisionId)
        {
            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep("Including item in data set representation");

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

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep("Including item in data set representation");
        }
    }
}
