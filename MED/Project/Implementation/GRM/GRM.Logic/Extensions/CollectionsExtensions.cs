using System.Collections.Generic;
using GRM.Logic.ProgressTracking;
using System.Linq;

namespace GRM.Logic.Extensions
{
    public static class CollectionsExtensions
    {
        private static readonly int a = ProgressTrackerContainer.CurrentProgressTracker.RegisterSubstep("intersect");

        public static int[] SortedIntersect(this int[] first, int[] second)
        {
            ProgressTrackerContainer.CurrentProgressTracker.EnterSubstep(a);

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

            ProgressTrackerContainer.CurrentProgressTracker.LeaveSubstep(a);

            return result.ToArray();
        }

        public static IList<int> SortedExcept(this IList<int> first, IList<int> second)
        {
            var result = new List<int>();

            var firstIndex = 0;
            var secondIndex = 0;

            while (firstIndex < first.Count)
            {
                var firstValue = first[firstIndex];
                var secondValue = second.Count > secondIndex ? second[secondIndex] : int.MaxValue;

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

            return result;
        }
    }
}
