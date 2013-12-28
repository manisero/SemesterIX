using Xunit;

namespace GRM.Logic.UnitTests.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy
{
    public class GetFirstLevelChildSupportTests
    {
        private int Execute(int itemTransactionIdsCount)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy().GetFirstLevelChildSupport(itemTransactionIdsCount);
        }

        [Fact]
        public void returns_back_item_transactionIds_count()
        {
            // Arrange
            var itemTransactionIds = 3;

            // Act
            var result = Execute(itemTransactionIds);

            // Assert
            Assert.Equal(itemTransactionIds, result);
        }
    }
}
