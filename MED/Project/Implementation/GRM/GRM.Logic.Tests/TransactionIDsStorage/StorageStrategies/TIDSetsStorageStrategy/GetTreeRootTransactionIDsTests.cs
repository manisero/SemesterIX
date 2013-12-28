using System.Collections.Generic;
using Xunit;

namespace GRM.Logic.UnitTests.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy
{
    public class GetTreeRootTransactionIDsTests
    {
        private IList<int> Execute(IList<int> allTransactionIds)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy().GetTreeRootTransactionIDs(allTransactionIds);
        }

        [Fact]
        public void returns_back_all_transaction_IDs()
        {
            // Arrange
            var allTransactionIds = new List<int> { 3, 5, 7 };

            // Act
            var result = Execute(allTransactionIds);

            // Assert
            Assert.Equal(allTransactionIds, result);
        }
    }
}
