using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.Extensions
{
    public static class GeneratorCollectionExtensions
    {
        public static IList<Generator> SortedIntersect(this IList<Generator> first, IList<Generator> second)
        {
            var result = new List<Generator>();

            var firstIndex = 0;
            var secondIndex = 0;

            while (firstIndex < first.Count && secondIndex < second.Count)
            {
                var candidate = first[firstIndex];
                var firstValue = candidate.GetIdentifier();
                var secondValue = second[secondIndex].GetIdentifier();

                if (firstValue > secondValue)
                {
                    secondIndex++;
                }
                else if (secondValue > firstValue)
                {
                    firstIndex++;
                }
                else
                {
                    result.Add(candidate);

                    firstIndex++;
                    secondIndex++;
                }
            }

            return result;
        }
    }
}