using System.Collections.Generic;
using Xunit;

namespace GRM.Logic.UnitTests.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy
{
    public class GetTreeRootTransactionIDsTests
    {
        private IList<int> Execute(IList<int> allTransactionIds)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy().GetTreeRootTransactionIDs(allTransactionIds);
        }

        [Fact]
        public void returns_empty_set()
        {
            // Arrange
            var allTransactionIds = new List<int> { 3, 5, 7 };

            // Act
            var result = Execute(allTransactionIds);

            // Assert
            Assert.Equal(new List<int>(), result);
        }
    }
}
