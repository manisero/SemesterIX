﻿using System.Collections.Generic;
using Xunit;

namespace GRM.Logic.UnitTests.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy
{
    public class GetFirstLevelChildTransactionIDsTests
    {
        private int[] Execute(IList<int> itemTransactionIds, int[] allTransactionIds)
        {
            return new Logic.GRMAlgorithm.TransactionIDsStorage.StorageStrategies.DiffSetsStorageStrategy().GetFirstLevelChildTransactionIDs(itemTransactionIds, allTransactionIds);
        }

        [Fact]
        public void returns_back_all_transaction_IDs_except_item_transaction_IDs()
        {
            // Arrange
            var itemTransactionIds = new List<int> { 3, 5, 7 };
            var allTransactionIds = new[] { 3, 4, 5, 6, 7 };

            // Act
            var result = Execute(itemTransactionIds, allTransactionIds);

            // Assert
            Assert.Equal(new[] { 4, 6 }, result);
        }
    }
}
