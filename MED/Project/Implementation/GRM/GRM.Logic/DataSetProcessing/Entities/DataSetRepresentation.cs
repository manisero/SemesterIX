using System.Collections.Generic;

namespace GRM.Logic.DataSetProcessing.Entities
{
    public class DataSetRepresentation
    {
        public IDictionary<string, int> DecisionIDs { get; set; }

        public IDictionary<int, int> TransactionDecisions { get; set; }

        public IDictionary<Item, ItemID> ItemIDs { get; set; }

        public IDictionary<ItemID, ItemInfo> ItemInfos { get; set; }
    }
}
