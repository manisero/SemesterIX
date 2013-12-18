using System.Collections.Generic;
using GRM.Logic.DatabaseProcessing;
using GRM.Logic.DatabaseProcessing.Entities;
using Xunit;
using System.Linq;

namespace GRM.Logic.Tests.DatabaseProcessing
{
    public class DatabaseRepresentationBuilderTests
    {
        private DatabaseRepresentation Execute(IEnumerable<ConcreteItem> database)
        {
            return new DatabaseRepresentationBuilder().Build(database);
        }

        private void AssertItemRepresentation(Item item, int expectedId, int[] expectedFrequencies, DatabaseRepresentation actualRepresentation)
        {
            Assert.True(actualRepresentation.ItemIDs.ContainsKey(item));
            Assert.Equal(expectedId, actualRepresentation.ItemIDs[item]);

            Assert.True(actualRepresentation.ItemTransactions.ContainsKey(expectedId));
            Assert.Equal(expectedFrequencies, actualRepresentation.ItemTransactions[expectedId].ToArray());
        }

        [Fact]
        public void represents_single_item()
        {
            // Arrange
            var item = new Item { Name = "Name", Value = "Value" };

            var database = new[]
                {
                    new ConcreteItem
                        {
                            Item = item,
                            TransactionID = 1
                        }
                };

            // Act
            var result = Execute(database);

            // Assert
            Assert.Equal(1, result.ItemIDs.Count);
            Assert.Equal(1, result.ItemTransactions.Count);
            AssertItemRepresentation(item, 1, new[] {1}, result);
        }

        [Fact]
        public void represents_two_items()
        {
            // Arrange
            var item1 = new Item { Name = "Name1", Value = "Value1" };
            var item2= new Item { Name = "Name2", Value = "Value2" };

            var database = new[]
                {
                    new ConcreteItem
                        {
                            Item = item1,
                            TransactionID = 1
                        },
                    new ConcreteItem
                        {
                            Item = item2,
                            TransactionID = 2
                        }
                };

            // Act
            var result = Execute(database);

            // Assert
            Assert.Equal(2, result.ItemIDs.Count);
            Assert.Equal(2, result.ItemTransactions.Count);
            AssertItemRepresentation(item1, 1, new[] { 1 }, result);
            AssertItemRepresentation(item2, 2, new[] { 2 }, result);
        }

        [Fact]
        public void represents_single_doubled_item()
        {
            // Arrange
            var item = new Item { Name = "Name", Value = "Value" };

            var database = new[]
                {
                    new ConcreteItem
                        {
                            Item = item,
                            TransactionID = 1
                        },
                    new ConcreteItem
                        {
                            Item = item,
                            TransactionID = 2
                        }
                };

            // Act
            var result = Execute(database);

            // Assert
            Assert.Equal(1, result.ItemIDs.Count);
            Assert.Equal(1, result.ItemTransactions.Count);
            AssertItemRepresentation(item, 1, new[] { 1, 2 }, result);
        }

        [Fact]
        public void represents_two_doubled_items()
        {
            // Arrange
            var item1 = new Item { Name = "Name1", Value = "Value1" };
            var item2 = new Item { Name = "Name2", Value = "Value2" };

            var database = new[]
                {
                    new ConcreteItem
                        {
                            Item = item1,
                            TransactionID = 1
                        },
                    new ConcreteItem
                        {
                            Item = item1,
                            TransactionID = 2
                        },
                    new ConcreteItem
                        {
                            Item = item2,
                            TransactionID = 2
                        },
                    new ConcreteItem
                        {
                            Item = item2,
                            TransactionID = 3
                        }
                };

            // Act
            var result = Execute(database);

            // Assert
            Assert.Equal(2, result.ItemIDs.Count);
            Assert.Equal(2, result.ItemTransactions.Count);
            AssertItemRepresentation(item1, 1, new[] { 1, 2 }, result);
            AssertItemRepresentation(item2, 2, new[] { 2, 3 }, result);
        }
    }
}
