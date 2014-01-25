using System.Collections.Generic;
using System.Linq;
using GRM.Logic.GRMAlgorithm.Entities;
using Xunit;

namespace GRM.Logic.UnitTests.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy
{
    public class SetChildTransactionIDsAndSupportTests
    {
        private void Execute(Node child, IDictionary<int, Node.DecisionTransactionIDs> parentDecisionTransactionIds, IDictionary<int, Node.DecisionTransactionIDs> parentSiblingDecisionsTransactionIds)
        {
            new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy()
                .SetChildTransactionIDsAndSupport(child,
                                                  new Node { DecisionsTransactionIDs = parentDecisionTransactionIds },
                                                  new Node { DecisionsTransactionIDs = parentSiblingDecisionsTransactionIds });
        }

        [Fact]
        public void sets_DecisionTransactionIDs()
        {
            // Arrange
            var child = new Node();
            var parentDecisionTransactionIds = new Dictionary<int, Node.DecisionTransactionIDs>
                {
                    { 10, new Node.DecisionTransactionIDs { Support = 3, TransactionIDs = new[] { 1, 4, 5 } } },
                    { 20, new Node.DecisionTransactionIDs { Support = 5, TransactionIDs = new[] { 2 } } },
                    { 30, new Node.DecisionTransactionIDs { Support = 4, TransactionIDs = new[] { 6 } } }
                };

            var parentSiblingDecisionTransactionIds = new Dictionary<int, Node.DecisionTransactionIDs>
                {
                    { 10, new Node.DecisionTransactionIDs { Support = 5, TransactionIDs = new[] { 1, 3, 4 } } },
                    { 30, new Node.DecisionTransactionIDs { Support = 7, TransactionIDs = new[] { 6 } } },
                    { 40, new Node.DecisionTransactionIDs { Support = 6, TransactionIDs = new[] { 8 } } }
                };

            // Act
            Execute(child, parentDecisionTransactionIds, parentSiblingDecisionTransactionIds);

            // Assert
            Assert.NotNull(child.DecisionsTransactionIDs);
            Assert.Equal(2, child.DecisionsTransactionIDs.Count);

            Assert.True(child.DecisionsTransactionIDs.ContainsKey(10));
            Assert.Equal(2, child.DecisionsTransactionIDs[10].Support);
            Assert.Equal(new List<int> { 3 }, child.DecisionsTransactionIDs[10].TransactionIDs);

            Assert.True(child.DecisionsTransactionIDs.ContainsKey(30));
            Assert.Equal(4, child.DecisionsTransactionIDs[30].Support);
            Assert.False(child.DecisionsTransactionIDs[30].TransactionIDs.Any());
        }
    }
}
