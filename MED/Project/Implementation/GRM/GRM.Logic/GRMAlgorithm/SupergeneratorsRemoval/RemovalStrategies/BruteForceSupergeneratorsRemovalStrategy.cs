using System.Collections.Generic;
using System.Linq;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.GRMAlgorithm.SupergeneratorsRemoval.RemovalStrategies
{
    public class BruteForceSupergeneratorsRemovalStrategy : ISupergeneratorsRemovalStrategy
    {
        public void Apply(IList<Generator> generators, Generator subgenerator)
        {
            var supergenerators = new List<Generator>();

            foreach (var generator in generators)
            {
                if (subgenerator.All(generator.Contains))
                {
                    supergenerators.Add(generator);
                }
            }

            foreach (var supergenerator in supergenerators)
            {
                generators.Remove(supergenerator);
            }
        }
    }
}
