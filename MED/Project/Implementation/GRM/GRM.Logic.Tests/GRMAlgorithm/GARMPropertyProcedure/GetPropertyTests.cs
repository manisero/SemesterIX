using System.Collections.Generic;
using GRM.Logic.GRMAlgorithm.Entities;
using Xunit;

namespace GRM.Logic.Tests.GRMAlgorithm.GARMPropertyProcedure
{
    public class GetPropertyTests
    {
        private GARMPropertyType Execute(IList<int> leftChildTransactionIds, IList<int> rightChildTransactionIds)
        {
            return new Logic.GRMAlgorithm._Impl.GARMPropertyProcedure().GetProperty(leftChildTransactionIds, rightChildTransactionIds);
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
        public void detects_difference()
        {
            // Arrange
            var leftChildTransactionIds = new[] { 1, 2, 5 };
            var rightChildTransactionIds = new[] { 2, 4, 5 };

            // Act
            var result = Execute(leftChildTransactionIds, rightChildTransactionIds);

            // Assert
            Assert.Equal(GARMPropertyType.Difference, result);
        }
    }
}
