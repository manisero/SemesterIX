using System.Collections.Generic;
using System.Linq;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.GRMAlgorithm.ItemsSorting.SortingStrategies
{
    public class ReverseLexicographicalSortingStrategy : ISortingStrategy
    {
        public IEnumerable<ItemInfo> Apply(IEnumerable<ItemInfo> items)
        {
            return items.OrderByDescending(x => x.AttributeID).ThenByDescending(x => x.ValueID);
        }
    }
}