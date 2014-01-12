using System.Collections.Generic;
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
                var isSupergenerator = true;

                foreach (var subitem in subgenerator)
                {
                    var containsSubitem = false;

                    foreach (var item in generator)
                    {
                        if (item.AttributeID == subitem.AttributeID && item.ValueID == subitem.ValueID)
                        {
                            containsSubitem = true;
                            break;
                        }
                    }

                    if (!containsSubitem)
                    {
                        isSupergenerator = false;
                        break;
                    }
                }

                if (isSupergenerator)
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