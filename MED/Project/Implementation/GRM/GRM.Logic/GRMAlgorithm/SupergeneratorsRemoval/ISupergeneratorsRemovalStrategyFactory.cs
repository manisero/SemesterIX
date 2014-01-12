namespace GRM.Logic.GRMAlgorithm.SupergeneratorsRemoval
{
    public interface ISupergeneratorsRemovalStrategyFactory
    {
        ISupergeneratorsRemovalStrategy Create(SupergeneratorsRemovalStrategyType strategyType);
    }
}
