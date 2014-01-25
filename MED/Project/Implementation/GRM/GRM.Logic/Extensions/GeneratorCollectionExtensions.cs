using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Logic.Extensions
{
    public static class GeneratorCollectionExtensions
    {
        public static SortedList<long, Generator> SortedIntersect(this SortedList<long, Generator> first, SortedList<long, Generator> second)
        {
            var result = new SortedList<long, Generator>();

            if (first.Count == 0 || second.Count == 0)
            {
                return result;
            }

            var firstIndex = 0;
            var secondIndex = 0;

            var candidate = first[first.Keys[firstIndex]];
            var firstValue = candidate.GetIdentifier();
            var secondValue = second[second.Keys[secondIndex]].GetIdentifier();

            while (true)
            {
                if (firstValue > secondValue)
                {
                    secondIndex++;

                    if (secondIndex == second.Count)
                    {
                        break;
                    }

                    secondValue = second[second.Keys[secondIndex]].GetIdentifier();
                }
                else if (secondValue > firstValue)
                {
                    firstIndex++;

                    if (firstIndex == first.Count)
                    {
                        break;
                    }

                    candidate = first[first.Keys[firstIndex]];
                    firstValue = candidate.GetIdentifier();
                }
                else
                {
                    result.Add(firstValue, candidate);

                    firstIndex++;
                    secondIndex++;

                    if (firstIndex == first.Count || secondIndex == second.Count)
                    {
                        break;
                    }

                    candidate = first[first.Keys[firstIndex]];
                    firstValue = candidate.GetIdentifier();
                    secondValue = second[second.Keys[secondIndex]].GetIdentifier();
                }
            }

            return result;
        }
    }
}