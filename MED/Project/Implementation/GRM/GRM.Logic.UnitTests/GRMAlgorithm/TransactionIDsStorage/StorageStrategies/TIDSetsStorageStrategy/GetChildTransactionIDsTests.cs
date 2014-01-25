﻿using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace GRM.Logic.UnitTests.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy
{
    public class GetChildTransactionIDsTests
    {
        private int[] Execute(int[] parentTransactionIds, int[] parentSiblingTransactionIds)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.TIDSetsStorageStrategy().GetChildTransactionIDs(parentTransactionIds, parentSiblingTransactionIds);
        }

        [Fact]
        public void for_equal_sets_returns_whole_set()
        {
            // Arrange
            var transactionIds = new[] { 3, 5, 7 };

            // Act
            var result = Execute(transactionIds, transactionIds);

            // Assert
            Assert.Equal(transactionIds, result);
        }

        [Fact]
        public void for_similar_sets_returns_intersection()
        {
            // Arrange
            var parentTransactionIds = new[] { 3, 5, 7 };
            var parentSiblingTransactionIds = new[] { 4, 5, 7, 9 };

            // Act
            var result = Execute(parentTransactionIds, parentSiblingTransactionIds);

            // Assert
            Assert.Equal(new[] { 5, 7 }, result);
        }

        [Fact]
        public void for_different_sets_returns_empty_set()
        {
            // Arrange
            var parentTransactionIds = new[] { 3, 5, 7 };
            var parentSiblingTransactionIds = new[] { 4, 6, 8 };

            // Act
            var result = Execute(parentTransactionIds, parentSiblingTransactionIds);

            // Assert
            Assert.Equal(0, result.Length);
        }
    }
}
