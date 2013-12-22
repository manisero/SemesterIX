using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm
{
    public interface ITreeBuilder
    {
        Tree Build(IEnumerable<ItemInfo> frequentItems, IEnumerable<int> decisionIds, IDictionary<int, int> transactionDecisions);
    }
}
