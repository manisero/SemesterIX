using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using Xunit;

namespace GRM.Logic.UnitTests.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy
{
    public class SetChildDecisivenessTests
    {
        private void Execute(Node child, IDictionary<int, int> transactionDecisions)
        {
            new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy().SetChildDecisiveness(child, null, transactionDecisions);
        }

        [Fact]
        public void sets_decisiveness_for_equal_decisions()
        {
            // Arrange
            var child = new Node { TransactionIDs = new List<int> { 1, 3 } };
            var transactionDecisions = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 1 } };

            // Act
            Execute(child, transactionDecisions);

            // Assert
            Assert.True(child.IsDecisive);
            Assert.Equal(1, child.DecisionID);
        }

        [Fact]
        public void sets_decisiveness_for_not_equal_decisions()
        {
            // Arrange
            var child = new Node { TransactionIDs = new List<int> { 1, 2, 3 } };
            var transactionDecisions = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 1 } };

            // Act
            Execute(child, transactionDecisions);

            // Assert
            Assert.False(child.IsDecisive);
            Assert.Equal(1, child.DecisionID);
        }

        [Fact]
        public void does_not_set_DecisionTransactionIDs()
        {
            // Arrange
            var child = new Node { TransactionIDs = new List<int> { 1, 2, 3 } };
            var transactionDecisions = new Dictionary<int, int> { { 1, 1 }, { 2, 1 }, { 3, 1 } };

            // Act
            Execute(child, transactionDecisions);

            // Assert
            Assert.Null(child.DecisionTransactionIDs);
        }
    }
}
