using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.ProgressTracking;

namespace GRM.Logic.DataSetProcessing._Impl
{
    public class TransactionProcessor : ITransactionProcessor
    {
        private readonly int _buildingDecisionDecisionIdDictionarySubstepId;
        private readonly int _buildingItemItemIDDictionarySybstepId;
        private readonly int _includingItemInDataSetRepresentationSubstepId;

        public TransactionProcessor()
        {
            _buildingDecisionDecisionIdDictionarySubstepId = ProgressTrackerContainer.CurrentProgressTracker.RegisterSubstep("Building decision -> decision id dictionary");
            _buildingItemItemIDDictionarySybstepId = ProgressTrackerContainer.CurrentProgressTracker.RegisterSubstep("Building item -> item id dictionary");
            _includingItemInDataSetRepresentationSubstepId = ProgressTrackerContainer.CurrentProgressTracker.RegisterSubstep("Including item in data set representation");
        }

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

                var item = items[i].Trim();

                if (item.Length == 0)
                {
                    continue;
                }

                var itemId = GetItemID(buildState, i, item);

                AppendItem(buildState, itemId, transactionId, decisionId);
            }
        }

        private int GetDecisionID(DataSetRepresentationBuildState buildState, string decision)
        {
            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(_buildingDecisionDecisionIdDictionarySubstepId);

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

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(_buildingDecisionDecisionIdDictionarySubstepId);

            return decisionId;
        }

        private ItemID GetItemID(DataSetRepresentationBuildState buildState, int attributeId, string attributeValue)
        {
            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(_buildingItemItemIDDictionarySybstepId);

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

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(_buildingItemItemIDDictionarySybstepId);

            return itemId;
        }

        private void AppendItem(DataSetRepresentationBuildState buildState, ItemID itemId, int transactionId, int decisionId)
        {
            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(_includingItemInDataSetRepresentationSubstepId);

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

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(_includingItemInDataSetRepresentationSubstepId);
        }
    }
}
