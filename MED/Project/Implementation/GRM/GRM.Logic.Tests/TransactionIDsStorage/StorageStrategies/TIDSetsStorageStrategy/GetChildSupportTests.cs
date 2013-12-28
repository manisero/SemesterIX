using System.Collections.Generic;
using Xunit;

namespace GRM.Logic.UnitTests.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy
{
    public class GetChildSupportTests
    {
        private int Execute(int parentSupport, IList<int> childTransactionIds)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy().GetChildSupport(parentSupport, childTransactionIds);
        }

        [Fact]
        public void returns_child_transactionIds_count()
        {
            // Arrange
            var childTransactionIds = new List<int> { 3, 5, 7 };

            // Act
            var result = Execute(100, childTransactionIds);

            // Assert
            Assert.Equal(3, result);
        }
    }
}
