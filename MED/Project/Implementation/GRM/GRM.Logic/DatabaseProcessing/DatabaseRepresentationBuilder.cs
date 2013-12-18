using System.Collections.Generic;
using GRM.Logic.DatabaseProcessing.Entities;

namespace GRM.Logic.DatabaseProcessing
{
    public class DatabaseRepresentationBuilder
    {
        public DatabaseRepresentation Build(IEnumerable<ConcreteItem> database)
        {
            var result = new DatabaseRepresentation();
            var mappingCounter = 1;

            foreach (var item in database)
            {
                int itemId;

                if (!result.ItemIDs.ContainsKey(item.Item))
                {
                    result.ItemIDs.Add(item.Item, mappingCounter);
                    itemId = mappingCounter;
                    mappingCounter++;
                }
                else
                {
                    itemId = result.ItemIDs[item.Item];
                }

                if (!result.ItemTransactions.ContainsKey(itemId))
                {
                    result.ItemTransactions.Add(itemId, new List<int> { item.TransactionID });
                }
                else
                {
                    result.ItemTransactions[itemId].Add(item.TransactionID);
                }
            }

            return result;
        }
    }
}
