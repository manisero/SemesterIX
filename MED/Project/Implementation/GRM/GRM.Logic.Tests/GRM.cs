using System.Collections.Generic;
using Xunit;
using System.Linq;

namespace GRM.Logic.Tests
{
    public struct Item
    {
        public string Name { get; set; }

        public string Value { get; set; }
    }

    public struct ConcreteItem
    {
        public Item Item { get; set; }

        public int TransactionID { get; set; }
    }

    public class DatabaseRepresentation
    {
        public IDictionary<Item, int> ItemIDs { get; private set; }

        public IDictionary<int, IList<int>> ItemTransactions { get; private set; }

        public DatabaseRepresentation()
        {
            ItemIDs = new Dictionary<Item, int>();
            ItemTransactions = new Dictionary<int, IList<int>>();
        }
    }

    public class GRM
    {
        private DatabaseRepresentation Execute(IEnumerable<ConcreteItem> database)
        {
            var result = new DatabaseRepresentation();
            var mappingCounter = 1;

            foreach (var item in database)
            {
                int itemId;

                if (!result.ItemIDs.ContainsKey(item.Item))
                {
                    result.ItemIDs.Add(item.Item, mappingCounter);
                    itemId = mappingCounter;
                    mappingCounter++;
                }
                else
                {
                    itemId = result.ItemIDs[item.Item];
                }

                if (!result.ItemTransactions.ContainsKey(itemId))
                {
                    result.ItemTransactions.Add(itemId, new List<int>{ item.TransactionID });
                }
                else
                {
                    result.ItemTransactions[itemId].Add(item.TransactionID);
                }
            }

            return result;
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
