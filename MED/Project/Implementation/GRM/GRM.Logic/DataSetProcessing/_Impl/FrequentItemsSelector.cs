using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.DataSetProcessing._Impl
{
    public class FrequentItemsSelector : IFrequentItemsSelector
    {
        public IEnumerable<ItemInfo> SelectFrequentItems(IEnumerable<ItemInfo> items, int minimumSupport)
        {
            var result = new List<ItemInfo>();

            foreach (var item in items)
            {
                if (item.TransactionIDs.Count >= minimumSupport)
                {
                    result.Add(item);
                }
            }

            return result;
        }
    }
}