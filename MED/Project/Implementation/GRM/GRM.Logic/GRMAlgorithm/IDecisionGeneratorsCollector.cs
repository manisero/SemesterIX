using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm
{
    public interface IDecisionGeneratorsCollector
    {
        void AppendDecisionGenerators(int decisionId, IList<Generator> generators);

        IDictionary<int, IList<Generator>> GetDecisionsGenerators();
    }
}