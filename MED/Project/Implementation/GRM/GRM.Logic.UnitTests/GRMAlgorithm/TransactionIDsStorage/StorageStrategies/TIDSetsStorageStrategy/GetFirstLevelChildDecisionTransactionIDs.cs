using System.Collections.Generic;
using Xunit;

namespace GRM.Logic.UnitTests.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy
{
    public class GetFirstLevelChildDecisionTransactionIDs
    {
        private IDictionary<int, IList<int>> Execute(IList<int> itemTransactionIds, IDictionary<int, int> transactionDecisions)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy().GetFirstLevelChildDecisionTransactionIDs(itemTransactionIds, transactionDecisions);
        }

        [Fact]
        public void always_returns_null()
        {
            // Arrange
            var itemTransactionIds = new List<int> { 1, 3 };
            var transactionDecisions = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 1 } };

            // Act
            var result = Execute(itemTransactionIds, transactionDecisions);

            // Assert
            Assert.Null(result);
        }
    }
}
