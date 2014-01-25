using System.Collections.Generic;

namespace GRM.Logic.Extensions
{
    public static class ListExtensions
    {
        public static IList<int> SortedIntersect(this IList<int> first, IList<int> second)
        {
            var result = new List<int>();

            var firstIndex = 0;
            var secondIndex = 0;

            while (firstIndex < first.Count && secondIndex < second.Count)
            {
                var firstValue = first[firstIndex];
                var secondValue = second[secondIndex];

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
                    result.Add(firstValue);

                    firstIndex++;
                    secondIndex++;
                }
            }

            return result;
        }
    }
}
