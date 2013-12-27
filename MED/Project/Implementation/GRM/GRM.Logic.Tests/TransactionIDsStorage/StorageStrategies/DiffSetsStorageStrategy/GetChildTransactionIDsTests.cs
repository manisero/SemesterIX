using System.Collections.Generic;
using Xunit;

namespace GRM.Logic.Tests.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy
{
    public class GetChildTransactionIDsTests
    {
        private IList<int> Execute(IList<int> parentTransactionIds, IEnumerable<int> parentSiblingTransactionIds)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy().GetChildTransactionIDs(parentTransactionIds, parentSiblingTransactionIds);
        }

        [Fact]
        public void for_equal_sets_returns_empty_set()
        {
            // Arrange
            var transactionIds = new List<int> { 3, 5, 7 };

            // Act
            var result = Execute(transactionIds, transactionIds);

            // Assert
            Assert.Equal(new List<int>(), result);
        }

        [Fact]
        public void for_similar_sets_returns_sibling_transaction_IDs_except_parent_transaction_IDs()
        {
            // Arrange
            var parentTransactionIds = new List<int> { 3, 5, 7, 10 };
            var parentSiblingTransactionIds = new List<int> { 4, 5, 7, 9 };

            // Act
            var result = Execute(parentTransactionIds, parentSiblingTransactionIds);

            // Assert
            Assert.Equal(new List<int> { 4, 9 }, result);
        }

        [Fact]
        public void for_different_sets_returns_sibling_transaction_IDs()
        {
            // Arrange
            var parentTransactionIds = new List<int> { 3, 5, 7 };
            var parentSiblingTransactionIds = new List<int> { 4, 6, 8 };

            // Act
            var result = Execute(parentTransactionIds, parentSiblingTransactionIds);

            // Assert
            Assert.Equal(parentSiblingTransactionIds, result);
        }
    }
}
