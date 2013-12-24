using System.Collections.Generic;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;
using Xunit;
using System.Linq;

namespace GRM.Logic.Tests.GRMAlgorithm.GARMPropertyProcedure
{
    public class ApplyPropertyTests
    {
        private Node Execute(GARMPropertyType property, Node leftChild, Node rightChild, IDictionary<int, int> transactionDecisions = null, int minimalSupport = 0)
        {
            // Arrange
            var parent = new Node
                {
                    Children = new List<Node> { new Node(), leftChild, new Node(), rightChild, new Node() }
                };

            // Act
            new Logic.GRMAlgorithm._Impl.GARMPropertyProcedure().ApplyProperty(property, parent, leftChild, rightChild,
                                                                               transactionDecisions ?? new Dictionary<int, int>(), minimalSupport);

            return parent;
        }

        [Fact]
        public void on_equality_removes_right_child_from_parent()
        {
            // Arrange
            var leftChild = new Node();
            var rightChild = new Node();

            // Act
            var parent = Execute(GARMPropertyType.Equality, leftChild, rightChild);

            // Assert
            Assert.Equal(4, parent.Children.Count);
            Assert.False(parent.Children.Contains(rightChild));
        }

        [Fact]
        public void on_subsumption_does_nothing()
        {
            // Arrange
            var leftChild = new Node();
            var rightChild = new Node();

            // Act
            var parent = Execute(GARMPropertyType.Subsumption, leftChild, rightChild);

            // Assert
            Assert.Equal(5, parent.Children.Count);
        }

        [Fact]
        public void on_difference_does_not_remove_right_child_from_parent()
        {
            // Arrange
            var leftChild = new Node
                {
                    TransactionIDs = new int[0]
                };

            var rightChild = new Node
                {
                    Generators = new List<Generator> { new Generator() },
                    TransactionIDs = new int[0]
                };

            // Act
            var parent = Execute(GARMPropertyType.Difference, leftChild, rightChild);

            // Assert
            Assert.Equal(5, parent.Children.Count);
            Assert.True(parent.Children.Contains(rightChild));
        }

        [Fact]
        public void on_difference_adds_right_child_copy_to_left_node_children()
        {
            // Arrange
            var leftChild = new Node
                {
                    TransactionIDs = new[] { 1 },
                    Children = new List<Node> { new Node() }
                };

            var rightChild = new Node
                {
                    Generators = new List<Generator> { new Generator { new ItemID { AttributeID = 1, ValueID = 1 }, new ItemID { AttributeID = 2, ValueID = 2 } } },
                    TransactionIDs = new[] { 1 }
                };

            var transactionIds = new Dictionary<int, int> { { 1, 1 } };

            // Act
            Execute(GARMPropertyType.Difference, leftChild, rightChild, transactionIds);

            // Assert
            Assert.Equal(2, leftChild.Children.Count);
            Assert.Equal(new[] { new Generator { new ItemID { AttributeID = 1, ValueID = 1 }, new ItemID { AttributeID = 2, ValueID = 2 } } },
                         leftChild.Children[1].Generators);
        }

        [Fact]
        public void on_difference_left_node_childs_TransactionIDs_is_intersection_of_left_child_and_right_child_TransactionIDs()
        {
            // Arrange
            var leftChild = new Node
                {
                    TransactionIDs = new[] { 1, 2, 3 },
                    Children = new List<Node> { new Node() }
                };

            var rightChild = new Node
                {
                    Generators = new List<Generator> { new Generator() },
                    TransactionIDs = new[] { 2, 3, 5 }
                };

            var transactionIds = new Dictionary<int, int> { { 2, 2 }, { 3, 3 } };

            // Act
            Execute(GARMPropertyType.Difference, leftChild, rightChild, transactionIds);

            // Assert
            Assert.Equal(new[] { 2, 3 }, leftChild.Children[1].TransactionIDs.ToArray());
        }

        [Fact]
        public void on_difference_detects_new_left_node_child_decisiveness()
        {
            // Arrange
            var leftChild = new Node
                {
                    TransactionIDs = new[] { 1, 2 },
                    Children = new List<Node> { new Node() }
                };

            var rightChild = new Node
                {
                    Generators = new List<Generator> { new Generator() },
                    TransactionIDs = new[] { 1, 2 }
                };

            var transactionIds = new Dictionary<int, int> { { 1, 0 }, { 2, 0 } };

            // Act
            Execute(GARMPropertyType.Difference, leftChild, rightChild, transactionIds);

            // Assert
            Assert.True(leftChild.Children[1].IsDecisive);
            Assert.Equal(0, leftChild.Children[1].DecisionID);
        }

        [Fact]
        public void on_difference_detects_new_left_node_child_indecisiveness()
        {
            // Arrange
            var leftChild = new Node
                {
                    TransactionIDs = new[] { 1, 2 },
                    Children = new List<Node> { new Node() }
                };

            var rightChild = new Node
                {
                    Generators = new List<Generator> { new Generator() },
                    TransactionIDs = new[] { 1, 2 }
                };

            var transactionIds = new Dictionary<int, int> { { 1, 1 }, { 2, 2 } };

            // Act
            Execute(GARMPropertyType.Difference, leftChild, rightChild, transactionIds);

            // Assert
            Assert.False(leftChild.Children[1].IsDecisive);
            Assert.Equal(1, leftChild.Children[1].DecisionID);
        }
    }
}
