using System.Collections.Generic;
using System.Linq;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.GRMAlgorithm.ItemsSorting.SortingStrategies
{
    public class LexicographicalSortingStrategy : ISortingStrategy
    {
        public IEnumerable<ItemInfo> Apply(IEnumerable<ItemInfo> items)
        {
            return items.OrderBy(x => x.AttributeID).ThenBy(x => x.ValueID);
        }
    }
}