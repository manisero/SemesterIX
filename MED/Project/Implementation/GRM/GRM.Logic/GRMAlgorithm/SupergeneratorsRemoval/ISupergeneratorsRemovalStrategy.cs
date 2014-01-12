using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm.SupergeneratorsRemoval
{
    public interface ISupergeneratorsRemovalStrategy
    {
        void Apply(IList<Generator> generators, Generator subgenerator);
    }
}
