using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;

namespace GRM.Logic.GRMAlgorithm.ItemsSorting
{
    public interface ISortingStrategy
    {
        IEnumerable<ItemInfo> Apply(IEnumerable<ItemInfo> items);
    }
}
