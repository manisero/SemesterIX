using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm
{
    public interface IResultBuilder
    {
        void AppendDecisionGenerators(int decisionId, IList<Generator> generators);

        GRMResult GetResult(IDictionary<string, int> decisionIds, IDictionary<Item, ItemID> itemIds);
    }
}