using GRM.Logic.ProgressTracking.ProgressTrackers;

namespace GRM.Logic.ProgressTracking._Impl
{
    public class ProgressTrackerFactory : IProgressTrackerFactory
    {
        public IProgressTracker Create(TrackingLevel trackingLevel)
        {
            switch (trackingLevel)
            {
                case TrackingLevel.NoTracking:
                    return new EmptyProgressTracker();
                case TrackingLevel.Task:
                    return new TaskProgressTracker();
                case TrackingLevel.Steps:
                    return new StepProgressTracker();
                case TrackingLevel.Substeps:
                    return new SubstepProgressTracker();
                default:
                    return null;
            }
        }
    }
}