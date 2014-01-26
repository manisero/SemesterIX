using GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting.Collectors;

namespace GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting._Impl
{
    public class DecisionGeneratorsCollectorFactory : IDecisionGeneratorsCollectorFactory
    {
        public IDecisionGeneratorsCollector Create(DecisionSupergeneratorsHandlingStrategyType strategyType)
        {
            switch (strategyType)
            {
                case DecisionSupergeneratorsHandlingStrategyType.InvertedLists:
                    return new InvertedListsDecisionGeneratorsCollector();
                case DecisionSupergeneratorsHandlingStrategyType.BruteForce:
                    return new BruteForceDecisionGeneratorsCollector();
                default:
                    return null;
            }
        }
    }
}