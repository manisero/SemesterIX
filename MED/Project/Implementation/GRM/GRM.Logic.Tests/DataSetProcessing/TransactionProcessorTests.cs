using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.DataSetProcessing._Impl;
using Xunit;
using System.Linq;

namespace GRM.Logic.Tests.DataSetProcessing
{
    public class TransactionProcessorTests
    {
        private void Execute(int transactionId, string transaction, DataSetRepresentationBuildState buildState)
        {
            new TransactionProcessor().AppendTransaction(transactionId, transaction, buildState);
        }

        private void AssertItemState(int attributeId, string attributeValue, int exptectedValueId,
                                     int[] expectedFrequencies, bool expectedIsDecisive, string expectedDecision,
                                     DataSetRepresentationBuildState actualBuildState)
        {
            var expectedItem = new Item { AttributeID = attributeId, Value = attributeValue };
            var expectedId = new ItemID { AttributeID = attributeId, ValueID = exptectedValueId };

            Assert.True(actualBuildState.ItemIDs.ContainsKey(expectedItem));
            Assert.Equal(expectedId, actualBuildState.ItemIDs[expectedItem]);

            Assert.True(actualBuildState.ItemInfos.ContainsKey(expectedId));
            Assert.Equal(expectedFrequencies, actualBuildState.ItemInfos[expectedId].TransactionIDs.ToArray());
            Assert.Equal(expectedIsDecisive, actualBuildState.ItemInfos[expectedId].IsDecisive);
            Assert.Equal(expectedDecision, actualBuildState.ItemInfos[expectedId].Decision);
        }

        [Fact]
        public void appends_single_item()
        {
            // Arrange
            var transactionId = 1;
            var transaction = "value,decision";
            var buildState = new DataSetRepresentationBuildState();

            // Act
            Execute(transactionId, transaction, buildState);

            // Assert
            Assert.Equal(1, buildState.ItemIDs.Count);
            Assert.Equal(1, buildState.ItemInfos.Count);
            AssertItemState(0, "value", 1, new[] {1}, true, "decision", buildState);
        }

        [Fact]
        public void appends_two_items()
        {
            // Arrange
            var transactionId = 1;
            var transaction = "value1,value2,decision";
            var buildState = new DataSetRepresentationBuildState();

            // Act
            Execute(transactionId, transaction, buildState);

            // Assert
            Assert.Equal(2, buildState.ItemIDs.Count);
            Assert.Equal(2, buildState.ItemInfos.Count);
            AssertItemState(0, "value1", 1, new[] { 1 }, true, "decision", buildState);
            AssertItemState(1, "value2", 2, new[] { 1 }, true, "decision", buildState);
        }

        [Fact]
        public void appends_single_doubled_item()
        {
            // Arrange
            var transactionId = 2;
            var transaction = "value,decision";
            var buildState = new DataSetRepresentationBuildState();
            buildState.ItemIDs[new Item { AttributeID = 0, Value = "value" }] = new ItemID { AttributeID = 0, ValueID = 1 };
            buildState.ItemInfos[new ItemID { AttributeID = 0, ValueID = 1 }] = new ItemInfo { TransactionIDs = new List<int> { 1 }, IsDecisive = true, Decision = "decision" };

            // Act
            Execute(transactionId, transaction, buildState);

            // Assert
            Assert.Equal(1, buildState.ItemIDs.Count);
            Assert.Equal(1, buildState.ItemInfos.Count);
            AssertItemState(0, "value", 1, new[] { 1, 2 }, true, "decision", buildState);
        }

        [Fact]
        public void appends_two_values_of_the_same_attribute()
        {
            // Arrange
            var transactionId = 2;
            var transaction = "value2,decision";
            var buildState = new DataSetRepresentationBuildState();
            buildState.ItemIDs[new Item { AttributeID = 0, Value = "value1" }] = new ItemID { AttributeID = 0, ValueID = 1 };
            buildState.ItemInfos[new ItemID { AttributeID = 0, ValueID = 1 }] = new ItemInfo { TransactionIDs = new List<int> { 1 }, IsDecisive = true, Decision = "decision" };
            buildState.ItemValueMappingCounter = 2;

            // Act
            Execute(transactionId, transaction, buildState);

            // Assert
            Assert.Equal(2, buildState.ItemIDs.Count);
            Assert.Equal(2, buildState.ItemInfos.Count);
            AssertItemState(0, "value1", 1, new[] { 1 }, true, "decision", buildState);
            AssertItemState(0, "value2", 2, new[] { 2 }, true, "decision", buildState);
        }

        [Fact]
        public void appends_two_doubled_items()
        {
            // Arrange
            var transactionId = 2;
            var transaction = "value1,value2,decision";
            var buildState = new DataSetRepresentationBuildState();
            buildState.ItemIDs[new Item { AttributeID = 0, Value = "value1" }] = new ItemID { AttributeID = 0, ValueID = 1 };
            buildState.ItemIDs[new Item { AttributeID = 1, Value = "value2" }] = new ItemID { AttributeID = 1, ValueID = 2 };
            buildState.ItemInfos[new ItemID { AttributeID = 0, ValueID = 1 }] = new ItemInfo { TransactionIDs = new List<int> { 1 }, IsDecisive = true, Decision = "decision" };
            buildState.ItemInfos[new ItemID { AttributeID = 1, ValueID = 2 }] = new ItemInfo { TransactionIDs = new List<int> { 1 }, IsDecisive = true, Decision = "decision" };
            buildState.ItemValueMappingCounter = 3;

            // Act
            Execute(transactionId, transaction, buildState);

            // Assert
            Assert.Equal(2, buildState.ItemIDs.Count);
            Assert.Equal(2, buildState.ItemInfos.Count);
            AssertItemState(0, "value1", 1, new[] { 1, 2 }, true, "decision", buildState);
            AssertItemState(1, "value2", 2, new[] { 1, 2 }, true, "decision", buildState);
        }

        [Fact]
        public void assigns_ValueID_properly()
        {
            // Arrange
            var transactionId = 2;
            var transaction = "value3,decision";
            var buildState = new DataSetRepresentationBuildState();
            buildState.ItemIDs[new Item { AttributeID = 0, Value = "value1" }] = new ItemID { AttributeID = 0, ValueID = 1 };
            buildState.ItemIDs[new Item { AttributeID = 1, Value = "value2" }] = new ItemID { AttributeID = 1, ValueID = 2 };
            buildState.ItemInfos[new ItemID { AttributeID = 0, ValueID = 1 }] = new ItemInfo { TransactionIDs = new List<int> { 1 }, IsDecisive = true, Decision = "decision" };
            buildState.ItemInfos[new ItemID { AttributeID = 1, ValueID = 2 }] = new ItemInfo { TransactionIDs = new List<int> { 1 }, IsDecisive = true, Decision = "decision" };
            buildState.ItemValueMappingCounter = 3;

            // Act
            Execute(transactionId, transaction, buildState);

            // Assert
            Assert.Equal(3, buildState.ItemIDs.Count);
            Assert.Equal(3, buildState.ItemInfos.Count);
            AssertItemState(0, "value3", 3, new[] { 2 }, true, "decision", buildState);
        }

        [Fact]
        public void assigns_IsDecisive_correctly()
        {
            // Arrange
            var transactionId = 2;
            var transaction = "value,decision2";
            var buildState = new DataSetRepresentationBuildState();
            buildState.ItemIDs[new Item { AttributeID = 0, Value = "value" }] = new ItemID { AttributeID = 0, ValueID = 1 };
            buildState.ItemInfos[new ItemID { AttributeID = 0, ValueID = 1 }] = new ItemInfo { TransactionIDs = new List<int> { 1 }, IsDecisive = true, Decision = "decision1" };

            // Act
            Execute(transactionId, transaction, buildState);

            // Assert
            Assert.Equal(1, buildState.ItemIDs.Count);
            Assert.Equal(1, buildState.ItemInfos.Count);
            AssertItemState(0, "value", 1, new[] { 1, 2 }, false, "decision1", buildState);
        }
    }
}
