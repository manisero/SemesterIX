namespace GRM.Logic.ProgressTracking
{
    public interface IProgressTrackerFactory
    {
        IProgressTracker Create(TrackingLevel trackingLevel);
    }
}
