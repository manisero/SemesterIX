using Xunit;

namespace GRM.Logic.UnitTests.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy
{
    public class GetTreeRootSupportTests
    {
        private int Execute(int allTransactionIdsCount)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy().GetTreeRootSupport(allTransactionIdsCount);
        }

        [Fact]
        public void retuntns_back_all_transaction_IDs_count()
        {
            // Arrange
            var allTransactionIdsCount = 3;

            // Act
            var result = Execute(allTransactionIdsCount);

            // Assert
            Assert.Equal(allTransactionIdsCount, result);
        }
    }
}
