using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using Xunit;

namespace GRM.Logic.UnitTests.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy
{
    public class GetFirstLevelChildDecisionTransactionIDs
    {
        private IDictionary<int, Node.DecisionTransactionIDs> Execute(IList<int> itemTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> rootDecisionsTransactionIds)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy().GetFirstLevelChildDecisionsTransactionIDs(itemTransactionIds, rootDecisionsTransactionIds);
        }

        [Fact]
        public void always_returns_null()
        {
            // Arrange
            var itemTransactionIds = new List<int> { 1, 3 };
            var transactionDecisions = new Dictionary<int, Node.DecisionTransactionIDs> { { 1, new Node.DecisionTransactionIDs() } };

            // Act
            var result = Execute(itemTransactionIds, transactionDecisions);

            // Assert
            Assert.Null(result);
        }
    }
}
