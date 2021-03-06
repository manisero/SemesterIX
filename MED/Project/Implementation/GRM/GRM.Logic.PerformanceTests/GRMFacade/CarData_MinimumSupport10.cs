﻿using System;
using GRM.Logic.ProgressTracking;
using GRM.Logic.UnitTests.GRMFacade;

namespace GRM.Logic.PerformanceTests.GRMFacade
{
    public class CarData_MinimumSupport10 : GRMFacadeTestsBase
    {
        protected override string DataSet
        {
            get { return Resources.Resources.car_100k; }
        }

        protected override int MinimumSupport
        {
            get { return 10; }
        }

        protected override void AssertResult(GRMResult result)
        {
            var taskInfo = ProgressTrackerContainer.CurrentProgressTracker.GetInfo();
            Console.WriteLine("Execution lasted: {0}", taskInfo.Duration);
        }
    }
}
