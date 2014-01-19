using GRM.Logic.GRMAlgorithm.ItemsSorting;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage;
using GRM.Logic.ProgressTracking;

namespace GRM.Presentation
{
    public class Options
    {
        public bool HelpRequested { get; set; }

        public string DataFilePath { get; set; }

        public int? MinimumSupport { get; set; }

        public bool DataFileContainsHeaders { get; set; }

        public int? DecisionAttributeIndex { get; set; }

        public SortingStrategyType SortingStrategy { get; set; }

        public TransactionIDsStorageStrategyType TransactionIdsStorageStrategy { get; set; }

        public TrackingLevel TrackingLevel { get; set; }

        public Options()
        {
            TrackingLevel = TrackingLevel.Task;
        }
    }
}
