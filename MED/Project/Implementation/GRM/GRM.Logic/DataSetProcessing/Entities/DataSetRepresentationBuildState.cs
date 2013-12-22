using System.Collections.Generic;

namespace GRM.Logic.DataSetProcessing.Entities
{
    public class DataSetRepresentationBuildState
    {
        public int ItemValueMappingCounter = 1;

        public IDictionary<Item, ItemID> ItemIDs = new Dictionary<Item, ItemID>();

        public IDictionary<ItemID, IList<int>> ItemTransactions = new Dictionary<ItemID, IList<int>>();
    }
}