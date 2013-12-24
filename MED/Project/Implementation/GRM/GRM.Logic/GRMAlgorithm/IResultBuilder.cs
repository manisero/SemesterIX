using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm._Impl;

namespace GRM.Logic.GRMAlgorithm
{
    public interface IResultBuilder
    {
        bool CanBuildResult(IDictionary<int, Generator> generators);

        GRMResult Build(IDictionary<int, Generator> generators, IDictionary<string, int> decisionIds, IDictionary<Item, ItemID> itemIds);
    }
}