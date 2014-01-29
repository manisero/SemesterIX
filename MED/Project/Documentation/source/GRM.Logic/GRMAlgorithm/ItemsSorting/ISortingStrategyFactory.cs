namespace GRM.Logic.GRMAlgorithm.ItemsSorting
{
    public interface ISortingStrategyFactory
    {
        ISortingStrategy Create(SortingStrategyType strategyType);
    }
}
