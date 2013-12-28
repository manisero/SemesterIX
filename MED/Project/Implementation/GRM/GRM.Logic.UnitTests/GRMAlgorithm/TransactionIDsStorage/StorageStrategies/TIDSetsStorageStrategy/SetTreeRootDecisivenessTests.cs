using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using Xunit;

namespace GRM.Logic.UnitTests.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy
{
    public class SetTreeRootDecisivenessTests
    {
        private void Execute(Node root, IDictionary<int, int> transactionDecisions)
        {
            new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy().SetTreeRootDecisiveness(root, transactionDecisions);
        }

        [Fact]
        public void sets_decisiveness_for_equal_decisions()
        {
            // Arrange
            var root = new Node();
            var transactionDecisions = new Dictionary<int, int> { { 1, 1 }, { 2, 1 }, { 3, 1 } };

            // Act
            Execute(root, transactionDecisions);

            // Assert
            Assert.True(root.IsDecisive);
            Assert.Equal(1, root.DecisionID);
        }

        [Fact]
        public void sets_decisiveness_for_not_equal_decisions()
        {
            // Arrange
            var root = new Node();
            var transactionDecisions = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 1 } };

            // Act
            Execute(root, transactionDecisions);

            // Assert
            Assert.False(root.IsDecisive);
            Assert.Equal(1, root.DecisionID);
        }

        [Fact]
        public void does_not_set_DecisionTransactionIDs()
        {
            // Arrange
            var root = new Node();
            var transactionDecisions = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 1 } };

            // Act
            Execute(root, transactionDecisions);

            // Assert
            Assert.Null(root.DecisionTransactionIDs);
        }
    }
}
