using System.Collections.Generic;

namespace GRM.Logic.DataSetProcessing.Entities
{
    public class DataSetRepresentation
    {
        public IDictionary<Item, ItemID> ItemIDs { get; private set; }

        public IDictionary<ItemID, IList<int>> ItemTransactions { get; private set; }

        public DataSetRepresentation()
        {
            ItemIDs = new Dictionary<Item, ItemID>();
            ItemTransactions = new Dictionary<ItemID, IList<int>>();
        }
    }
}
