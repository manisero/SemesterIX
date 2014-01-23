﻿//using System.Collections.Generic;
//using GRM.Logic.GRMAlgorithm.Entities;
//using Xunit;

//namespace GRM.Logic.UnitTests.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy
//{
//    public class SetChildDecisivenessTests
//    {
//        private void Execute(Node child, IDictionary<int, IList<int>> parentDecisionTransactionIds)
//        {
//            new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy().SetChildDecisiveness(child, parentDecisionTransactionIds, null);
//        }

//        [Fact]
//        public void sets_DecisionTransactionIDs()
//        {
//            // Arrange
//            var child = new Node { TransactionIDs = new List<int> { 1, 2, 3 } };
//            var parentDecisionTransactionIds = new Dictionary<int, IList<int>>
//                {
//                    { 10, new List<int> { 1, 4, 5 } },
//                    { 20, new List<int> { 2 } },
//                    { 30, new List<int> { 6 } }
//                };

//            // Act
//            Execute(child, parentDecisionTransactionIds);

//            // Assert
//            Assert.NotNull(child.DecisionsTransactionIDs);
//            Assert.Equal(2, child.DecisionsTransactionIDs.Count);

//            Assert.True(child.DecisionsTransactionIDs.ContainsKey(10));
//            Assert.Equal(new List<int> { 4, 5 }, child.DecisionsTransactionIDs[10]);
            
//            Assert.True(child.DecisionsTransactionIDs.ContainsKey(30));
//            Assert.Equal(new List<int> { 6 }, child.DecisionsTransactionIDs[30]);
//        }

//        [Fact]
//        public void sets_decisiveness_for_equal_decisions()
//        {
//            // Arrange
//            var child = new Node { TransactionIDs = new List<int> { 1, 2, 3 } };
//            var parentDecisionTransactionIds = new Dictionary<int, IList<int>>
//                {
//                    { 10, new List<int> { 1, 3 } },
//                    { 20, new List<int> { 6 } },
//                    { 30, new List<int> { 2 } }
//                };

//            // Act
//            Execute(child, parentDecisionTransactionIds);

//            // Assert
//            Assert.True(child.IsDecisive);
//            Assert.Equal(20, child.DecisionID);
//        }

//        [Fact]
//        public void sets_decisiveness_for_not_equal_decisions()
//        {
//            // Arrange
//            var child = new Node { TransactionIDs = new List<int> { 1, 2, 3 } };
//            var parentDecisionTransactionIds = new Dictionary<int, IList<int>>
//                {
//                    { 10, new List<int> { 1, 4, 5 } },
//                    { 20, new List<int> { 2 } },
//                    { 30, new List<int> { 6 } }
//                };

//            // Act
//            Execute(child, parentDecisionTransactionIds);

//            // Assert
//            Assert.False(child.IsDecisive);
//            Assert.Equal(10, child.DecisionID);
//        }
//    }
//}
