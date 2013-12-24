using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm
{
    public interface IResultBuilder
    {
        bool CanBuildResult(IDictionary<int, IList<Generator>> generators);

        GRMResult Build(IDictionary<int, IList<Generator>> generators, IDictionary<string, int> decisionIds, IDictionary<Item, ItemID> itemIds);
    }
}