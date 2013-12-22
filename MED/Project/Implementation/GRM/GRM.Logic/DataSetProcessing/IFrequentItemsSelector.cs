using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.DataSetProcessing
{
    public interface IFrequentItemsSelector
    {
        IDictionary<ItemID, ItemInfo> SelectFrequentItems(IEnumerable<ItemInfo> items, int minimumSupport);
    }
}
