using GRM.Logic.GRMAlgorithm.SupergeneratorsRemoval.RemovalStrategies;

namespace GRM.Logic.GRMAlgorithm.SupergeneratorsRemoval._Impl
{
    public class SupergeneratorsRemovalStrategyFactory : ISupergeneratorsRemovalStrategyFactory
    {
        public ISupergeneratorsRemovalStrategy Create(SupergeneratorsRemovalStrategyType strategyType)
        {
            switch (strategyType)
            {
                case SupergeneratorsRemovalStrategyType.BruteForce:
                    return new BruteForceSupergeneratorsRemovalStrategy();
                default:
                    return null;
            }
        }
    }
}