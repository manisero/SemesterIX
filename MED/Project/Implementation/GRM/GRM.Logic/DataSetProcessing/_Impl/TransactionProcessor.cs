using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.DataSetProcessing._Impl
{
    public class TransactionProcessor : ITransactionProcessor
    {
        public DataSetRepresentation Build(IEnumerable<ConcreteItem> dataSet)
        {
            var result = new DataSetRepresentation();
            var itemNamesIds = new Dictionary<string, int>();

            var itemNameMappingCounter = 1;
            var itemValueMappingCounter = 1;

            foreach (var item in dataSet)
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
