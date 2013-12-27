using System.IO;
using System.Text;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm.ItemsSorting;
using Xunit;
using System.Linq;

namespace GRM.Logic.Tests
{
    public class GRMFacadeTests
    {
        private GRMResult Execute(string dataSet, int minimumSupport, SortingStrategyType sortingStrategy)
        {
            using (var dataSetStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(dataSet)))
            {
                return new GRMFacade().ExecuteGRM(dataSetStream, minimumSupport, sortingStrategy, new ProgressInfo());
            }
        }

        private void AssertGeneratorIsInRule(Rule rule, params Item[] expectedGenerator)
        {
            Assert.True(rule.Generators.Any(x => x.Count() == expectedGenerator.Length && x.All(expectedGenerator.Contains)));
        }

        [Fact]
        public void for_car_data_set_finds_rules_for_unacc_and_acc()
        {
            // Act
            var result = Execute(Resources.CarDataSet, 10, SortingStrategyType.DescendingSupport);

            // Assert
            Assert.Equal(2, result.Rules.Count());
            Assert.True(result.Rules.Any(x => x.Decision == "unacc"));
            Assert.True(result.Rules.Any(x => x.Decision == "acc"));
        }

        [Fact]
        public void for_car_data_set_finds_generators_for_unacc()
        {
            // Act
            var result = Execute(Resources.CarDataSet, 10, SortingStrategyType.DescendingSupport);

            // Assert
            var rule = result.Rules.Single(x => x.Decision == "unacc");
            Assert.Equal(9, rule.Generators.Count);

            AssertGeneratorIsInRule(rule, new Item { AttributeID = 3, Value = "2" });

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 1, Value = "vhigh" },
                                    new Item { AttributeID = 4, Value = "small" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 0, Value = "med" },
                                    new Item { AttributeID = 1, Value = "high" },
                                    new Item { AttributeID = 4, Value = "small" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 0, Value = "high" },
                                    new Item { AttributeID = 4, Value = "small" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 2, Value = "2" },
                                    new Item { AttributeID = 3, Value = "more" },
                                    new Item { AttributeID = 4, Value = "small" });

            AssertGeneratorIsInRule(rule, new Item { AttributeID = 5, Value = "low" });

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 1, Value = "vhigh" },
                                    new Item { AttributeID = 2, Value = "2" },
                                    new Item { AttributeID = 4, Value = "med" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 0, Value = "high" },
                                    new Item { AttributeID = 2, Value = "2" },
                                    new Item { AttributeID = 4, Value = "med" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 0, Value = "high" },
                                    new Item { AttributeID = 1, Value = "vhigh" });
        }

        [Fact]
        public void for_car_data_set_finds_generators_for_acc()
        {
            // Act
            var result = Execute(Resources.CarDataSet, 10, SortingStrategyType.DescendingSupport);

            // Assert
            var rule = result.Rules.Single(x => x.Decision == "acc");
            Assert.Equal(7, rule.Generators.Count);

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 0, Value = "low" },
                                    new Item { AttributeID = 1, Value = "high" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 0, Value = "med" },
                                    new Item { AttributeID = 1, Value = "med" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "med" });

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 0, Value = "med" },
                                    new Item { AttributeID = 1, Value = "vhigh" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "high" });

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 0, Value = "high" },
                                    new Item { AttributeID = 1, Value = "high" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "high" });

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 0, Value = "med" },
                                    new Item { AttributeID = 1, Value = "high" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "high" });

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 0, Value = "high" },
                                    new Item { AttributeID = 1, Value = "med" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "high" });

            AssertGeneratorIsInRule(rule,
                                    new Item { AttributeID = 0, Value = "high" },
                                    new Item { AttributeID = 1, Value = "low" },
                                    new Item { AttributeID = 3, Value = "4" },
                                    new Item { AttributeID = 5, Value = "high" });
        }
    }
}
