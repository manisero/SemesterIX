using System.IO;
using System.Text;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm.ItemsSorting;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage;
using Xunit;
using System.Linq;

namespace GRM.Logic.Tests.GRMFacade
{
    public class GRMFacadeTests_CarDataSet
    {
        private void Execute(SortingStrategyType sortingStrategy, TransactionIDsStorageStrategyType transactionIdsStorageStrategy)
        {
            // Act
            GRMResult result;

            using (var dataSetStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(Resources.CarDataSet)))
            {
                result = new Logic.GRMFacade(sortingStrategy, transactionIdsStorageStrategy).ExecuteGRM(dataSetStream, 10, new ProgressInfo());
            }

            // Assert
            Assert.Equal(2, result.Rules.Count());

            // Assert rule for unacc
            Assert.True(result.Rules.Any(x => x.Decision == "unacc"));
            var unaccRule = result.Rules.Single(x => x.Decision == "unacc");
            Assert.Equal(13, unaccRule.Generators.Count);

            AssertGeneratorIsInRule(unaccRule, new Item { AttributeID = 3, Value = "2" });

            AssertGeneratorIsInRule(unaccRule,
                                    new Item { AttributeID = 0, Value = "vhigh" },
                                    new Item { AttributeID = 4, Value = "small" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(unaccRule,
                                    new Item { AttributeID = 1, Value = "vhigh" },
                                    new Item { AttributeID = 4, Value = "small" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(unaccRule,
                                    new Item { AttributeID = 0, Value = "med" },
                                    new Item { AttributeID = 1, Value = "high" },
                                    new Item { AttributeID = 4, Value = "small" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(unaccRule,
                                    new Item { AttributeID = 0, Value = "high" },
                                    new Item { AttributeID = 4, Value = "small" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(unaccRule,
                                    new Item { AttributeID = 2, Value = "2" },
                                    new Item { AttributeID = 3, Value = "more" },
                                    new Item { AttributeID = 4, Value = "small" });

            AssertGeneratorIsInRule(unaccRule, new Item { AttributeID = 5, Value = "low" });

            AssertGeneratorIsInRule(unaccRule,
                                    new Item { AttributeID = 0, Value = "vhigh" },
                                    new Item { AttributeID = 2, Value = "2" },
                                    new Item { AttributeID = 4, Value = "med" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(unaccRule,
                                    new Item { AttributeID = 1, Value = "vhigh" },
                                    new Item { AttributeID = 2, Value = "2" },
                                    new Item { AttributeID = 4, Value = "med" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(unaccRule,
                                    new Item { AttributeID = 0, Value = "high" },
                                    new Item { AttributeID = 2, Value = "2" },
                                    new Item { AttributeID = 4, Value = "med" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(unaccRule,
                                    new Item { AttributeID = 0, Value = "vhigh" },
                                    new Item { AttributeID = 1, Value = "vhigh" });

            AssertGeneratorIsInRule(unaccRule,
                                    new Item { AttributeID = 0, Value = "vhigh" },
                                    new Item { AttributeID = 1, Value = "high" });

            AssertGeneratorIsInRule(unaccRule,
                                    new Item { AttributeID = 0, Value = "high" },
                                    new Item { AttributeID = 1, Value = "vhigh" });

            // Assert rule for acc
            Assert.True(result.Rules.Any(x => x.Decision == "acc"));
            var accRule = result.Rules.Single(x => x.Decision == "acc");
            Assert.Equal(10, accRule.Generators.Count);

            AssertGeneratorIsInRule(accRule,
                                    new Item { AttributeID = 0, Value = "low" },
                                    new Item { AttributeID = 1, Value = "high" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(accRule,
                                    new Item { AttributeID = 0, Value = "med" },
                                    new Item { AttributeID = 1, Value = "med" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(accRule,
                                    new Item { AttributeID = 0, Value = "vhigh" },
                                    new Item { AttributeID = 1, Value = "med" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "high" });

            AssertGeneratorIsInRule(accRule,
                                    new Item { AttributeID = 0, Value = "vhigh" },
                                    new Item { AttributeID = 1, Value = "low" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "high" });

            AssertGeneratorIsInRule(accRule,
                                    new Item { AttributeID = 0, Value = "med" },
                                    new Item { AttributeID = 1, Value = "vhigh" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "high" });

            AssertGeneratorIsInRule(accRule,
                                    new Item { AttributeID = 0, Value = "low" },
                                    new Item { AttributeID = 1, Value = "vhigh" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "high" });

            AssertGeneratorIsInRule(accRule,
                                    new Item { AttributeID = 0, Value = "high" },
                                    new Item { AttributeID = 1, Value = "high" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "high" });

            AssertGeneratorIsInRule(accRule,
                                    new Item { AttributeID = 0, Value = "med" },
                                    new Item { AttributeID = 1, Value = "high" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "high" });

            AssertGeneratorIsInRule(accRule,
                                    new Item { AttributeID = 0, Value = "high" },
                                    new Item { AttributeID = 1, Value = "med" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "high" });

            AssertGeneratorIsInRule(accRule,
                                    new Item { AttributeID = 0, Value = "high" },
                                    new Item { AttributeID = 1, Value = "low" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "high" });
        }

        private void AssertGeneratorIsInRule(Rule rule, params Item[] expectedGenerator)
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
