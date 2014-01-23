using System.Collections.Generic;
using Xunit;

namespace GRM.Logic.UnitTests.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy
{
    public class GetFirstLevelChildDecisionTransactionIDs
    {
        private IDictionary<int, IList<int>> Execute(IList<int> itemTransactionIds, IDictionary<int, int> transactionDecisions)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy().GetFirstLevelChildDecisionsTransactionIDs(itemTransactionIds, transactionDecisions);
        }

        [Fact]
        public void returns_TransactionIDs_gruped_by_decision()
        {
            // Arrange
            var itemTransactionIds = new List<int> { 1, 3, 4 };
            var transactionDecisions = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 2 }, { 4, 1 } };

            // Act
            var result = Execute(itemTransactionIds, transactionDecisions);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(2, result.Count);

            Assert.True(result.ContainsKey(1));
            Assert.Equal(new List<int> { 1, 4 }, result[1]);
            
            Assert.True(result.ContainsKey(2));
            Assert.Equal(new List<int> { 3 }, result[2]);
        }
    }
}
