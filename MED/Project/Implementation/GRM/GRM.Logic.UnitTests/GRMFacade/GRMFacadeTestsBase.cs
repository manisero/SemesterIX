using System.IO;
using System.Linq;
using System.Text;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.ItemsSorting;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage;
using GRM.Logic.ProgressTracking;
using GRM.Logic.ProgressTracking.ProgressTrackers;
using Xunit;

namespace GRM.Logic.UnitTests.GRMFacade
{
    public abstract class GRMFacadeTestsBase
    {
        protected abstract string DataSet { get; }

        protected abstract int MinimumSupport { get; }

        protected virtual int? DecisionAttributeIndex
        {
            get { return null; }
        }

        private void Execute(SortingStrategyType sortingStrategy, TransactionIDsStorageStrategyType transactionIdsStorageStrategy)
        {
            // Arrange
            ProgressTrackerContainer.CurrentProgressTracker = new SubstepProgressTracker();

            // Act
            GRMResult result;

            using (var dataSetStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(DataSet)))
            {
                result = new Logic.GRMFacade(sortingStrategy, transactionIdsStorageStrategy, 0).ExecuteGRM(dataSetStream, false, DecisionAttributeIndex, MinimumSupport);
            }

            // Assert
            AssertResult(result);
        }

        protected abstract void AssertResult(GRMResult result);

        protected void AssertGeneratorIsInRule(Rule rule, params Item[] expectedGenerator)
        {
            Assert.True(rule.Generators.Any(x => x.Count() == expectedGenerator.Length && x.All(expectedGenerator.Contains)));
        }

        [Fact]
        public void works_properly_for_sorting_by_DescendingSupport_and_TIDSets_storage()
        {
            Execute(SortingStrategyType.DescendingSupport, TransactionIDsStorageStrategyType.TIDSets);
        }

        [Fact]
        public void works_properly_for_sorting_by_AscendingSupport_and_TIDSets_storage()
        {
            Execute(SortingStrategyType.AscendingSupport, TransactionIDsStorageStrategyType.TIDSets);
        }

        [Fact]
        public void works_properly_for_sorting_Lexicographically_and_TIDSets_storage()
        {
            Execute(SortingStrategyType.Lexicographical, TransactionIDsStorageStrategyType.TIDSets);
        }

        [Fact]
        public void works_properly_for_sorting_ReverseLexicographically_and_TIDSets_storage()
        {
            Execute(SortingStrategyType.ReverseLexicographical, TransactionIDsStorageStrategyType.TIDSets);
        }

        [Fact]
        public void works_properly_for_sorting_by_DescendingSupport_and_DiffSets_storage()
        {
            Execute(SortingStrategyType.DescendingSupport, TransactionIDsStorageStrategyType.DiffSets);
        }

        [Fact]
        public void works_properly_for_sorting_by_AscendingSupport_and_DiffSets_storage()
        {
            Execute(SortingStrategyType.AscendingSupport, TransactionIDsStorageStrategyType.DiffSets);
        }

        [Fact]
        public void works_properly_for_sorting_Lexicographically_and_DiffSets_storage()
        {
            Execute(SortingStrategyType.Lexicographical, TransactionIDsStorageStrategyType.DiffSets);
        }

        [Fact]
        public void works_properly_for_sorting_ReverseLexicographically_and_DiffSets_storage()
        {
            Execute(SortingStrategyType.ReverseLexicographical, TransactionIDsStorageStrategyType.DiffSets);
        }
    }
}