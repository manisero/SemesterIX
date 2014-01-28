using System;
using GRM.Logic.ProgressTracking;
using GRM.Logic.UnitTests.GRMFacade;

namespace GRM.Logic.PerformanceTests.GRMFacade
{
    public class MushroomsData_MinimumSupport5 : GRMFacadeTestsBase
    {
        protected override string DataSet
        {
            get { return Resources.Resources.mushrooms; }
        }

        protected override int MinimumSupport
        {
            get { return 5; }
        }

        protected override int? DecisionAttributeIndex
        {
            get { return 0; }
        }

        protected override void AssertResult(GRMResult result)
        {
            var taskInfo = ProgressTrackerContainer.CurrentProgressTracker.GetInfo();
            Console.WriteLine("Execution lasted: {0}", taskInfo.Duration);
        }
    }
}
