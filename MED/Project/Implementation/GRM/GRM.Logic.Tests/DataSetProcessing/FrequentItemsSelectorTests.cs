using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.DataSetProcessing._Impl;
using Xunit;

namespace GRM.Logic.Tests.DataSetProcessing
{
    public class FrequentItemsSelectorTests
    {
        private IEnumerable<ItemInfo> Execute(int minumumSupport, params IEnumerable<ItemInfo>[] itemGroups)
        {
            var items = new List<ItemInfo>();

            foreach (var @group in itemGroups)
            {
                items.AddRange(@group);
            }

            return new FrequentItemsSelector().SelectFrequentItems(items, minumumSupport);
        }

        [Fact]
        public void selects_any_item_for_min_sup_0()
        {
            // Arrange
            var items = new[]
                {
                    new ItemInfo { TransactionIDs = new int[0] },
                    new ItemInfo { TransactionIDs = new[] { 1 } },
                    new ItemInfo { TransactionIDs = new[] { 1, 2, 3 } }
                };

            // Act
            var result = Execute(0, items);

            // Assert
            Assert.Equal(items, result);
        }

        [Fact]
        public void selects_items_with_one_or_more_transactionIds_for_min_sup_1()
        {
            // Arrange
            var nonFrequentItems = new List<ItemInfo>
                {
                    new ItemInfo { TransactionIDs = new int[0] }
                };

            var frequentItems = new[]
                {
                    new ItemInfo { TransactionIDs = new[] { 1 } },
                    new ItemInfo { TransactionIDs = new[] { 1, 2, 3 } }
                };

            // Act
            var result = Execute(1, nonFrequentItems, frequentItems);

            // Assert
            Assert.Equal(frequentItems, result);
        }

        [Fact]
        public void selects_item_with_three_transactionIds_for_min_sup_2()
        {
            // Arrange
            var nonFrequentItems = new List<ItemInfo>
                {
                    new ItemInfo { TransactionIDs = new int[0] },
                    new ItemInfo { TransactionIDs = new[] { 1 } }
                };

            var frequentItems = new[]
                {
                    new ItemInfo { TransactionIDs = new[] { 1, 2, 3 } }
                };

            // Act
            var result = Execute(2, nonFrequentItems, frequentItems);

            // Assert
            Assert.Equal(frequentItems, result);
        }
    }
}
