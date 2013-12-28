using System.Collections.Generic;
using Xunit;

namespace GRM.Logic.UnitTests.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy
{
    public class GetChildTransactionIDsTests
    {
        private IList<int> Execute(IList<int> parentTransactionIds, IEnumerable<int> parentSiblingTransactionIds)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy().GetChildTransactionIDs(parentTransactionIds, parentSiblingTransactionIds);
        }

        [Fact]
        public void for_equal_sets_returns_whole_set()
        {
            // Arrange
            var transactionIds = new List<int> { 3, 5, 7 };

            // Act
            var result = Execute(transactionIds, transactionIds);

            // Assert
            Assert.Equal(transactionIds, result);
        }

        [Fact]
        public void for_similar_sets_returns_intersection()
        {
            // Arrange
            var parentTransactionIds = new List<int> { 3, 5, 7 };
            var parentSiblingTransactionIds = new List<int> { 4, 5, 7, 9 };

            // Act
            var result = Execute(parentTransactionIds, parentSiblingTransactionIds);

            // Assert
            Assert.Equal(new List<int> { 5, 7 }, result);
        }

        [Fact]
        public void for_different_sets_returns_empty_set()
        {
            // Arrange
            var parentTransactionIds = new List<int> { 3, 5, 7 };
            var parentSiblingTransactionIds = new List<int> { 4, 6, 8 };

            // Act
            var result = Execute(parentTransactionIds, parentSiblingTransactionIds);

            // Assert
            Assert.Equal(new List<int>(), result);
        }
    }
}
