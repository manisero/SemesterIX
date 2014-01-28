using GRM.Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies;

namespace GRM.Logic.GRMAlgorithm.TransactionIDsStorage._Impl
{
    public class TransactionIDsStorageStrategyFactory : ITransactionIDsStorageStrategyFactory
    {
        public ITransactionIDsStorageStrategy Create(TransactionIDsStorageStrategyType strategyType)
        {
            switch (strategyType)
            {
                case TransactionIDsStorageStrategyType.TIDSets:
                    return new TIDSetsStorageStrategy();
                case TransactionIDsStorageStrategyType.DiffSets:
                    return new DiffSetsStorageStrategy();
                default:
                    return null;
            }
        }
    }
}