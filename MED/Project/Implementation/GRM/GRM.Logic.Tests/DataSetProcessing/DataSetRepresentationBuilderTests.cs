using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.DataSetProcessing._Impl;
using Xunit;
using System.Linq;

namespace GRM.Logic.Tests.DataSetProcessing
{
    public class TransactionProcessorTests
    {
        private DataSetRepresentation Execute(IEnumerable<ConcreteItem> dataSet)
        {
            return new TransactionProcessor().Build(dataSet);
        }

        private void AssertItemRepresentation(Item item, int exptectedNameId, int exptectedValueId, int[] expectedFrequencies, DataSetRepresentation actualRepresentation)
        {
            var expectedId = new ItemID { NameID = exptectedNameId, ValueID = exptectedValueId };

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

            var dataSet = new[]
                {
                    new ConcreteItem
                        {
                            Item = item,
                            TransactionID = 1
                        }
                };

            // Act
            var result = Execute(dataSet);

            // Assert
            Assert.Equal(1, result.ItemIDs.Count);
            Assert.Equal(1, result.ItemTransactions.Count);
            AssertItemRepresentation(item, 1, 1, new[] {1}, result);
        }

        [Fact]
        public void represents_two_items()
        {
            // Arrange
            var item1 = new Item { Name = "Name1", Value = "Value1" };
            var item2= new Item { Name = "Name2", Value = "Value2" };

            var dataSet = new[]
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
            var result = Execute(dataSet);

            // Assert
            Assert.Equal(2, result.ItemIDs.Count);
            Assert.Equal(2, result.ItemTransactions.Count);
            AssertItemRepresentation(item1, 1, 1, new[] { 1 }, result);
            AssertItemRepresentation(item2, 2, 2, new[] { 2 }, result);
        }

        [Fact]
        public void represents_single_doubled_item()
        {
            // Arrange
            var item = new Item { Name = "Name", Value = "Value" };

            var dataSet = new[]
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
            var result = Execute(dataSet);

            // Assert
            Assert.Equal(1, result.ItemIDs.Count);
            Assert.Equal(1, result.ItemTransactions.Count);
            AssertItemRepresentation(item, 1, 1, new[] { 1, 2 }, result);
        }

        [Fact]
        public void represents_two_values_of_the_same_attribute()
        {
            // Arrange
            var item1 = new Item { Name = "Name", Value = "Value1" };
            var item2 = new Item { Name = "Name", Value = "Value2" };

            var dataSet = new[]
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
            var result = Execute(dataSet);

            // Assert
            Assert.Equal(2, result.ItemIDs.Count);
            Assert.Equal(2, result.ItemTransactions.Count);
            AssertItemRepresentation(item1, 1, 1, new[] { 1 }, result);
            AssertItemRepresentation(item2, 1, 2, new[] { 2 }, result);
        }

        [Fact]
        public void represents_two_doubled_items()
        {
            // Arrange
            var item1 = new Item { Name = "Name1", Value = "Value1" };
            var item2 = new Item { Name = "Name2", Value = "Value2" };

            var dataSet = new[]
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
            var result = Execute(dataSet);

            // Assert
            Assert.Equal(2, result.ItemIDs.Count);
            Assert.Equal(2, result.ItemTransactions.Count);
            AssertItemRepresentation(item1, 1, 1, new[] { 1, 2 }, result);
            AssertItemRepresentation(item2, 2, 2, new[] { 2, 3 }, result);
        }

        [Fact]
        public void represents_complex_case()
        {
            // Arrange
            var item1_1 = new Item { Name = "Name1", Value = "Value1" };
            var item2 = new Item { Name = "Name2", Value = "Value2" };
            var item1_2 = new Item { Name = "Name1", Value = "Value2" };

            var dataSet = new[]
                {
                    new ConcreteItem
                        {
                            Item = item1_1,
                            TransactionID = 1
                        },
                    new ConcreteItem
                        {
                            Item = item2,
                            TransactionID = 2
                        },
                    new ConcreteItem
                        {
                            Item = item1_1,
                            TransactionID = 2
                        },
                    new ConcreteItem
                        {
                            Item = item1_2,
                            TransactionID = 4
                        },
                    new ConcreteItem
                        {
                            Item = item2,
                            TransactionID = 3
                        },
                    new ConcreteItem
                    {
                        Item = item1_2,
                        TransactionID = 3
                    }
                };

            // Act
            var result = Execute(dataSet);

            // Assert
            Assert.Equal(3, result.ItemIDs.Count);
            Assert.Equal(3, result.ItemTransactions.Count);
            AssertItemRepresentation(item1_1, 1, 1, new[] { 1, 2 }, result);
            AssertItemRepresentation(item2, 2, 2, new[] { 2, 3 }, result);
            AssertItemRepresentation(item1_2, 1, 3, new[] { 4, 3 }, result);
        }
    }
}
