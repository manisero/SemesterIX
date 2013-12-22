using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.DataSetProcessing
{
    public interface IFrequentItemsSelector
    {
        IEnumerable<ItemInfo> SelectFrequentItems(IEnumerable<ItemInfo> items, int minimumSupport);
    }
}
