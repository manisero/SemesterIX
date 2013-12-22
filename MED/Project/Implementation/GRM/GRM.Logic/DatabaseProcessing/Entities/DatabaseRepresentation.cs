using System.Collections.Generic;

namespace GRM.Logic.DatabaseProcessing.Entities
{
    public class DatabaseRepresentation
    {
        public IDictionary<Item, ItemID> ItemIDs { get; private set; }

        public IDictionary<ItemID, IList<int>> ItemTransactions { get; private set; }

        public DatabaseRepresentation()
        {
            ItemIDs = new Dictionary<Item, ItemID>();
            ItemTransactions = new Dictionary<ItemID, IList<int>>();
        }
    }
}
