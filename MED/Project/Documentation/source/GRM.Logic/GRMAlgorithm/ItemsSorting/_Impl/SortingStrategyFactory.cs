using GRM.Logic.GRMAlgorithm.ItemsSorting.SortingStrategies;

namespace GRM.Logic.GRMAlgorithm.ItemsSorting._Impl
{
    public class SortingStrategyFactory : ISortingStrategyFactory
    {
        public ISortingStrategy Create(SortingStrategyType strategyType)
        {
            switch (strategyType)
            {
                case SortingStrategyType.AscendingSupport:
                    return new AscendingSupportSortingStrategy();
                case SortingStrategyType.DescendingSupport:
                    return new DescendingSupportSortingStrategy();
                case SortingStrategyType.Lexicographical:
                    return new LexicographicalSortingStrategy();
                default:
                    return null;
            }
        }
    }
}