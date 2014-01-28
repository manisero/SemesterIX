using System.Collections.Generic;

namespace GRM.Logic.DataSetProcessing.Entities
{
    public class DataSetRepresentationBuildState
    {
        public int DecisionMappingCounter = 1;

        public IDictionary<string, int> DecisionIDs = new Dictionary<string, int>();

        public IDictionary<int, int> TransactionDecisions = new Dictionary<int, int>();

        public int ItemValueMappingCounter = 1;

        public IDictionary<Item, ItemID> ItemIDs = new Dictionary<Item, ItemID>();

        public IDictionary<ItemID, ItemInfo> ItemInfos = new Dictionary<ItemID, ItemInfo>();
    }
}