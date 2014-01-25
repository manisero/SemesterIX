using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using Xunit;

namespace GRM.Logic.UnitTests.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy
{
    public class GetFirstLevelChildDecisionTransactionIDs
    {
        private IDictionary<int, Node.DecisionTransactionIDs> Execute(IList<int> itemTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> rootDecisionsTransactionIDs)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy().GetFirstLevelChildDecisionsTransactionIDs(itemTransactionIds, rootDecisionsTransactionIDs);
        }

        [Fact]
        public void returns_TransactionIDs_gruped_by_decision()
        {
            // Arrange
            var itemTransactionIds = new List<int> { 1, 3 };
            var rootDecisionsTransactionIDs = new Dictionary<int, Node.DecisionTransactionIDs>
                {
                    { 1, new Node.DecisionTransactionIDs { Support = 2, TransactionIDs = new[] { 1, 4 } } },
                    { 2, new Node.DecisionTransactionIDs { Support = 1, TransactionIDs = new[] { 2 } } }
                };

            // Act
            var result = Execute(itemTransactionIds, rootDecisionsTransactionIDs);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(1, result.Count);

            Assert.True(result.ContainsKey(1));
            Assert.Equal(new[] { 4 }, result[1].TransactionIDs);

            Assert.False(result.ContainsKey(2));
        }
    }
}
