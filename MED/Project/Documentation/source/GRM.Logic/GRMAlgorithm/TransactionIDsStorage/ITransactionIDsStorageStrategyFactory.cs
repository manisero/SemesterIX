namespace GRM.Logic.GRMAlgorithm.TransactionIDsStorage
{
    public interface ITransactionIDsStorageStrategyFactory
    {
        ITransactionIDsStorageStrategy Create(TransactionIDsStorageStrategyType strategyType);
    }
}
