using System.Collections.Generic;

namespace GRM.Logic.Extensions
{
    public static class LongListExtensions
    {
        public static IList<long> SortedIntersect(this IList<long> first, IList<long> second)
        {
            var result = new List<long>();

            if (first.Count == 0 || second.Count == 0)
            {
                return result;
            }

            var firstIndex = 0;
            var secondIndex = 0;

            var firstValue = first[firstIndex];
            var secondValue = second[secondIndex];

            while (true)
            {
                if (firstValue > secondValue)
                {
                    secondIndex++;

                    if (secondIndex == second.Count)
                    {
                        break;
                    }

                    secondValue = second[secondIndex];
                }
                else if (secondValue > firstValue)
                {
                    firstIndex++;

                    if (firstIndex == first.Count)
                    {
                        break;
                    }

                    firstValue = first[firstIndex];
                }
                else
                {
                    result.Add(firstValue);

                    firstIndex++;
                    secondIndex++;

                    if (firstIndex == first.Count || secondIndex == second.Count)
                    {
                        break;
                    }

                    firstValue = first[firstIndex];
                    secondValue = second[secondIndex];
                }
            }

            return result;
        }
    }
}