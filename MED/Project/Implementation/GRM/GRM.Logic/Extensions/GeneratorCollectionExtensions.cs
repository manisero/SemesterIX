using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.Extensions
{
    public static class GeneratorCollectionExtensions
    {
        public static SortedList<int, Generator> SortedIntersect(this SortedList<int, Generator> first, SortedList<int, Generator> second)
        {
            var result = new SortedList<int, Generator>();

            var firstIndex = 0;
            var secondIndex = 0;

            while (firstIndex < first.Count && secondIndex < second.Count)
            {
                var candidate = first[first.Keys[firstIndex]];
                var firstValue = candidate.GetIdentifier();
                var secondValue = second[second.Keys[secondIndex]].GetIdentifier();

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
                    result.Add(firstValue, candidate);

                    firstIndex++;
                    secondIndex++;
                }
            }

            return result;
        }
    }
}