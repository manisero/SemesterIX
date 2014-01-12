using GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting.Collectors;

namespace GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting._Impl
{
    public class DecisionGeneratorsCollectorFactory : IDecisionGeneratorsCollectorFactory
    {
        public IDecisionGeneratorsCollector Create(DecisionSupergeneratorsHandlingStrategyType strategyType)
        {
            switch (strategyType)
            {
                case DecisionSupergeneratorsHandlingStrategyType.BruteForce:
                    return new BruteForceDecisionGeneratorsCollector();
                case DecisionSupergeneratorsHandlingStrategyType.BruteForceLINQ:
                    return new BruteForceLINQDecisionGeneratorsCollector();
                default:
                    return null;
            }
        }
    }
}