using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using Xunit;

namespace GRM.Logic.UnitTests.GRMAlgorithm.GARMPropertyProcedure
{
    public class GetPropertyTests
    {
        private GARMPropertyType Execute(IList<int> leftChildTransactionIds, IList<int> rightChildTransactionIds)
        {
            ProgressInfoContainer.CurrentProgressInfo = new ProgressInfo();

            return new Logic.GRMAlgorithm._Impl.GARMPropertyProcedure(null).GetProperty(leftChildTransactionIds, rightChildTransactionIds);
        }

        [Fact]
        public void detects_equality()
        {
            // Arrange
            var leftChildTransactionIds = new[] { 1, 2, 3, 4, 5 };
            var rightChildTransactionIds = new[] { 1, 2, 3, 4, 5 };

            // Act
            var result = Execute(leftChildTransactionIds, rightChildTransactionIds);

            // Assert
            Assert.Equal(GARMPropertyType.Equality, result);
        }

        [Fact]
        public void detects_left_to_right_subsumption()
        {
            // Arrange
            var leftChildTransactionIds = new[] { 2, 3, 5 };
            var rightChildTransactionIds = new[] { 1, 2, 3, 4, 5 };

            // Act
            var result = Execute(leftChildTransactionIds, rightChildTransactionIds);

            // Assert
            Assert.Equal(GARMPropertyType.Subsumption, result);
        }

        [Fact]
        public void detects_right_to_left_subsumption()
        {
            // Arrange
            var leftChildTransactionIds = new[] { 1, 2, 3, 4, 5 };
            var rightChildTransactionIds = new[] { 1, 4, 5 };

            // Act
            var result = Execute(leftChildTransactionIds, rightChildTransactionIds);

            // Assert
            Assert.Equal(GARMPropertyType.Subsumption, result);
        }

        [Fact]
        public void detects_difference_for_single_items()
        {
            // Arrange
            var leftChildTransactionIds = new[] { 1 };
            var rightChildTransactionIds = new[] { 2 };

            // Act
            var result = Execute(leftChildTransactionIds, rightChildTransactionIds);

            // Assert
            Assert.Equal(GARMPropertyType.Difference, result);
        }

        [Fact]
        public void detects_difference_on_several_positions()
        {
            // Arrange
            var leftChildTransactionIds = new[] { 1, 2, 5 };
            var rightChildTransactionIds = new[] { 2, 4, 5 };

            // Act
            var result = Execute(leftChildTransactionIds, rightChildTransactionIds);

            // Assert
            Assert.Equal(GARMPropertyType.Difference, result);
        }

        [Fact]
        public void detects_difference_on_last_position()
        {
            // Arrange
            var leftChildTransactionIds = new[] { 1, 2 };
            var rightChildTransactionIds = new[] { 1, 3 };

            // Act
            var result = Execute(leftChildTransactionIds, rightChildTransactionIds);

            // Assert
            Assert.Equal(GARMPropertyType.Difference, result);
        }
    }
}
