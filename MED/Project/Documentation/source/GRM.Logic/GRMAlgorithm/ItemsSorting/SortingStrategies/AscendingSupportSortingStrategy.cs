using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using System.Linq;

namespace GRM.Logic.GRMAlgorithm.ItemsSorting.SortingStrategies
{
    public class AscendingSupportSortingStrategy : ISortingStrategy
    {
        public IEnumerable<ItemInfo> Apply(IEnumerable<ItemInfo> items)
        {
            return items.OrderBy(x => x.TransactionIDs.Count);
        }
    }
}
