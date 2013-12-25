using GRM.Logic.GRMAlgorithm.ItemsSorting.SortingStrategies;

namespace GRM.Logic.GRMAlgorithm.ItemsSorting._Impl
{
    public class SortingStrategyFactory : ISortingStrategyFactory
    {
        public ISortingStrategy Create(SortingStrategyType strategyType)
        {
            switch (strategyType)
            {
                case SortingStrategyType.Lexicographical:
                    return new LexicographicalSortingStrategy();
                case SortingStrategyType.ReverseLexicographical:
                    return new ReverseLexicographicalSortingStrategy();
                case SortingStrategyType.DescendingSupport:
                    return new DescendingSupportSortingStrategy();
                case SortingStrategyType.AscendingSupport:
                    return new AscendingSupportSortingStrategy();
                default:
                    return null;
            }
        }
    }
}