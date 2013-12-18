using System.Collections.Generic;

namespace GRM.Logic.DatabaseProcessing.Entities
{
    public class DatabaseRepresentation
    {
        public IDictionary<Item, int> ItemIDs { get; private set; }

        public IDictionary<int, IList<int>> ItemTransactions { get; private set; }

        public DatabaseRepresentation()
        {
            ItemIDs = new Dictionary<Item, int>();
            ItemTransactions = new Dictionary<int, IList<int>>();
        }
    }
}
