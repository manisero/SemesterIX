using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using Xunit;

namespace GRM.Logic.Tests.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy
{
    public class SetTreeRootDecisivenessTests
    {
        private void Execute(IDictionary<int, int> transactionDecisions, Node root)
        {
            new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy().SetTreeRootDecisiveness(transactionDecisions, root);
        }

        [Fact]
        public void sets_decisiveness_for_equal_decisions()
        {
            // Arrange
            var transactionDecisions = new Dictionary<int, int> { { 1, 1 }, { 2, 1 }, { 3, 1 } };
            var root = new Node();

            // Act
            Execute(transactionDecisions, root);

            // Assert
            Assert.True(root.IsDecisive);
            Assert.Equal(1, root.DecisionID);
        }

        [Fact]
        public void sets_decisiveness_for_not_equal_decisions()
        {
            // Arrange
            var transactionDecisions = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 1 } };
            var root = new Node();

            // Act
            Execute(transactionDecisions, root);

            // Assert
            Assert.False(root.IsDecisive);
            Assert.Equal(1, root.DecisionID);
        }

        [Fact]
        public void does_not_set_DecisionTransactionIDs()
        {
            // Arrange
            var transactionDecisions = new Dictionary<int, int> { { 1, 1 }, { 2, 2 }, { 3, 1 } };
            var root = new Node();

            // Act
            Execute(transactionDecisions, root);

            // Assert
            Assert.Null(root.DecisionTransactionIDs);
        }
    }
}
