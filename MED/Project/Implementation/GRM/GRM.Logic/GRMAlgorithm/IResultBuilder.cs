using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm
{
    public interface IResultBuilder
    {
        bool CanBuildResult(IDictionary<int, IEnumerable<ItemID>> generators);

        GRMResult Build(IDictionary<int, IEnumerable<ItemID>> generators, IDictionary<string, int> decisionIds, IDictionary<Item, ItemID> itemIds);
    }
}