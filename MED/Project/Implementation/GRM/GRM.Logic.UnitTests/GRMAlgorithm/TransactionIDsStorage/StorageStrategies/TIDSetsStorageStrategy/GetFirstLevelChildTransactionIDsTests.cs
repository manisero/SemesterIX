using System.Collections.Generic;
using Xunit;

namespace GRM.Logic.UnitTests.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy
{
    public class GetFirstLevelChildTransactionIDsTests
    {
        private int[] Execute(IList<int> itemTransactionIds, IList<int> allTransactionIds)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy().GetFirstLevelChildTransactionIDs(itemTransactionIds, allTransactionIds);
        }

        [Fact]
        public void returns_back_item_transaction_IDs()
        {
            // Arrange
            var itemTransactionIds = new List<int> { 3, 5, 7 };
            var allTransactionIds = new List<int> { 3, 4, 5, 6, 7 };

            // Act
            var result = Execute(itemTransactionIds, allTransactionIds);

            // Assert
            Assert.Equal(itemTransactionIds.ToArray(), result);
        }
    }
}
