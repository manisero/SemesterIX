using System.Collections.Generic;
using GRM.Logic.DatabaseProcessing.Entities;

namespace GRM.Logic.DatabaseProcessing._Impl
{
    public class DatabaseRepresentationBuilder : IDatabaseRepresentationBuilder
    {
        public DatabaseRepresentation Build(IEnumerable<ConcreteItem> database)
        {
            var result = new DatabaseRepresentation();
            var itemNamesIds = new Dictionary<string, int>();

            var itemNameMappingCounter = 1;
            var itemValueMappingCounter = 1;

            foreach (var item in database)
            {
                ItemID itemId;

                if (!result.ItemIDs.ContainsKey(item.Item))
                {
                    int itemNameId;

                    if (!itemNamesIds.ContainsKey(item.Item.Name))
                    {
                        itemNameId = itemNameMappingCounter;
                        itemNamesIds.Add(item.Item.Name, itemNameMappingCounter);

                        itemNameMappingCounter++;
                    }
                    else
                    {
                        itemNameId = itemNamesIds[item.Item.Name];
                    }

                    itemId = new ItemID
                        {
                            NameID = itemNameId,
                            ValueID = itemValueMappingCounter
                        };

                    result.ItemIDs.Add(item.Item, itemId);
                    itemValueMappingCounter++;
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
