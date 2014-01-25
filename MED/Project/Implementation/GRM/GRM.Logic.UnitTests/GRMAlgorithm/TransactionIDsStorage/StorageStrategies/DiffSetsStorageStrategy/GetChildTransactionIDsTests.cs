using System.Collections.Generic;
using Xunit;

namespace GRM.Logic.UnitTests.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy
{
    public class GetChildTransactionIDsTests
    {
        private int[] Execute(int[] parentTransactionIds, int[] parentSiblingTransactionIds)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy().GetChildTransactionIDs(parentTransactionIds, parentSiblingTransactionIds);
        }

        [Fact]
        public void for_equal_sets_returns_empty_set()
        {
            // Arrange
            var transactionIds = new[] { 3, 5, 7 };

            // Act
            var result = Execute(transactionIds, transactionIds);

            // Assert
            Assert.Equal(0, result.Length);
        }

        [Fact]
        public void for_similar_sets_returns_sibling_transaction_IDs_except_parent_transaction_IDs()
        {
            // Arrange
            var parentTransactionIds = new[] { 3, 5, 7, 10 };
            var parentSiblingTransactionIds = new[] { 4, 5, 7, 9 };

            // Act
            var result = Execute(parentTransactionIds, parentSiblingTransactionIds);

            // Assert
            Assert.Equal(new List<int> { 4, 9 }, result);
        }

        [Fact]
        public void for_different_sets_returns_sibling_transaction_IDs()
        {
            // Arrange
            var parentTransactionIds = new[] { 3, 5, 7 };
            var parentSiblingTransactionIds = new[] { 4, 6, 8 };

            // Act
            var result = Execute(parentTransactionIds, parentSiblingTransactionIds);

            // Assert
            Assert.Equal(parentSiblingTransactionIds, result);
        }
    }
}
