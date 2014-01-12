using GRM.Logic.DataSetProcessing;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.ProgressTracking;
using Xunit;
using System.Linq;

namespace GRM.Logic.UnitTests.GRMFacade
{
    public class GRMFacadeTests_ResearchReportDataSet : GRMFacadeTestsBase
    {
        protected override string DataSet
        {
            get { return Resources.ResearchReportDataSet; }
        }

        protected override int MinimumSupport
        {
            get { return 3; }
        }

        protected override void AssertResult(GRMResult result, ProgressInfo progressInfo)
        {
            Assert.Equal(1, result.Rules.Count());

            Assert.True(result.Rules.Any(x => x.Decision == "+"));
            var unaccRule = result.Rules.Single(x => x.Decision == "+");
            Assert.Equal(2, unaccRule.Generators.Count);

            AssertGeneratorIsInRule(unaccRule,
                                    new Item { AttributeID = 0, Value = "a" },
                                    new Item { AttributeID = 1, Value = "b" });

            AssertGeneratorIsInRule(unaccRule,
                                    new Item { AttributeID = 1, Value = "b" },
                                    new Item { AttributeID = 3, Value = "d" });
        }
    }
}
