using System.Collections.Generic;

namespace GRM.Logic.Extensions
{
    public enum SetsRelationType
    {
        Equality,
        FirstSubsumesSecond,
        SecondSubsumesFirst,
        Difference
    }

    public static class ArrayExtensions
    {
        public static SetsRelationType SortedGetSetsRelation(this int[] first, int[] second)
        {
            var firstSubsumesSecond = true;
            var secondSubsumesFirst = true;

            var firstIndex = 0;
            var secondIndex = 0;

            while (firstIndex < first.Length || secondIndex < second.Length)
            {
                var firstValue = GetValueOrNull(first, firstIndex);
                var secondValue = GetValueOrNull(second, secondIndex);

                if (firstValue == null || firstValue > secondValue)
                {
                    firstSubsumesSecond = false;
                    secondIndex++;
                }
                else if (secondValue == null || secondValue > firstValue)
                {
                    secondSubsumesFirst = false;
                    firstIndex++;
                }
                else
                {
                    firstIndex++;
                    secondIndex++;
                }

                if (!firstSubsumesSecond && !secondSubsumesFirst)
                {
                    return SetsRelationType.Difference;
                }
            }

            if (secondSubsumesFirst && firstSubsumesSecond)
            {
                return SetsRelationType.Equality;
            }
            else if (firstSubsumesSecond)
            {
                return SetsRelationType.FirstSubsumesSecond;
            }
            else
            {
                return SetsRelationType.SecondSubsumesFirst;
            }
        }

        private static int? GetValueOrNull(int[] array, int index)
        {
            return index < array.Length ? array[index] : (int?)null;
        }

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
