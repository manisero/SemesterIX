using System.Collections.Generic;
using Xunit;

namespace GRM.Logic.UnitTests.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy
{
    public class GetChildSupportTests
    {
        private int Execute(int parentSupport, IList<int> childTransactionIds)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy().GetChildSupport(parentSupport, childTransactionIds);
        }

        [Fact]
        public void returns_parent_support_minus_child_transaction_ids_count()
        {
            // Arrange
            var parentSupport = 5;
            var childTransactionIds = new List<int> { 3, 5, 7 };

            // Act
            var result = Execute(parentSupport, childTransactionIds);

            // Assert
            Assert.Equal(2, result);
        }
    }
}
