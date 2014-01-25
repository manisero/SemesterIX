using System.Collections.Generic;

namespace GRM.Logic.Extensions
{
    public static class ArrayExtensions
    {
        public static int[] SortedIntersect(this int[] first, int[] second)
        {
            var result = new List<int>();

            var firstIndex = 0;
            var secondIndex = 0;

            while (firstIndex < first.Length && secondIndex < second.Length)
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

            return result.ToArray();
        }

        public static int[] SortedExcept(this int[] first, int[] second)
        {
            var result = new List<int>();

            var firstIndex = 0;
            var secondIndex = 0;

            while (firstIndex < first.Length)
            {
                var firstValue = first[firstIndex];
                var secondValue = second.Length > secondIndex ? second[secondIndex] : int.MaxValue;

                if (firstValue > secondValue)
                {
                    secondIndex++;
                }
                else if (secondValue > firstValue)
                {
                    result.Add(firstValue);

                    firstIndex++;
                }
                else
                {
                    firstIndex++;
                    secondIndex++;
                }
            }

            return result.ToArray();
        }
    }
}
