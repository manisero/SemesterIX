using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.ProgressTracking;
using Xunit;
using System.Linq;

namespace GRM.Logic.UnitTests.GRMFacade
{
    public class GRMFacadeTests_CarDataSet : GRMFacadeTestsBase
    {
        protected override string DataSet
        {
            get { return Resources.CarDataSet; }
        }

        protected override int MinimumSupport
        {
            get { return 10; }
        }

        protected override void AssertResult(GRMResult result, IProgressTracker progressTracker)
        {
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
    }
}
