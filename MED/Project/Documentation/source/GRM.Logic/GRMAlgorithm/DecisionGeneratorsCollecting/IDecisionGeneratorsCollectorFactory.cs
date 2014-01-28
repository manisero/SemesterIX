namespace GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting
{
    public interface IDecisionGeneratorsCollectorFactory
    {
        IDecisionGeneratorsCollector Create(DecisionSupergeneratorsHandlingStrategyType strategyType);
    }
}
