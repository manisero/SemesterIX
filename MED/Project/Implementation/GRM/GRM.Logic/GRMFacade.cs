using System.IO;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.DataSetProcessing._Impl;
using GRM.Logic.GRMAlgorithm;
using GRM.Logic.GRMAlgorithm.ItemsSorting;
using GRM.Logic.GRMAlgorithm.ItemsSorting._Impl;
using GRM.Logic.GRMAlgorithm.SupergeneratorsRemoval;
using GRM.Logic.GRMAlgorithm.SupergeneratorsRemoval._Impl;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage._Impl;
using GRM.Logic.GRMAlgorithm._Impl;
using GRM.Logic.ProgressTracking;

namespace GRM.Logic
{
    public class GRMFacade
    {
        private readonly IDataSetRepresentationBuilder _dataSetRepresentationBuilder;
        private readonly IFrequentItemsSelector _frequentItemsSelector;
        private readonly ISortingStrategy _sortingStrategy;
        private readonly ITreeBuilder _treeBuilder;
        private readonly IDecisionGeneratorsCollector _decisionGeneratorsCollector;
        private readonly IGARMProcedure _garmProcedure;
        private readonly GRMResultBuilder _grmResultBuilder;

        public GRMFacade(SortingStrategyType sortingStrategy, TransactionIDsStorageStrategyType transactionIdsStorageStrategy, SupergeneratorsRemovalStrategyType supergeneratorsRemovalStrategy)
        {
            _dataSetRepresentationBuilder = new DataSetRepresentationBuilder(new TransactionProcessor());
            _frequentItemsSelector = new FrequentItemsSelector();
            _sortingStrategy = new SortingStrategyFactory().Create(sortingStrategy);

            var storageStrategy = new TransactionIDsStorageStrategyFactory().Create(transactionIdsStorageStrategy);
            _treeBuilder = new TreeBuilder(storageStrategy);
            _decisionGeneratorsCollector = new DecisionGeneratorsCollector(new SupergeneratorsRemovalStrategyFactory().Create(supergeneratorsRemovalStrategy));
            _garmProcedure = new GARMProcedure(_decisionGeneratorsCollector, new GARMPropertyProcedure(storageStrategy));

            _grmResultBuilder = new GRMResultBuilder();
        }

        public GRMResult ExecuteGRM(Stream dataSetStream, bool dataContainsHeaders, int? decisionAttributeIndex, int minimumSupport)
        {
            var progressTracker = ProgressTrackerContainer.CurrentProgressTracker;

            progressTracker.BeginTask();

            progressTracker.BeginStep("Creating data set representation");
            var representation = _dataSetRepresentationBuilder.Build(dataSetStream, dataContainsHeaders, decisionAttributeIndex);
            progressTracker.EndStep();

            progressTracker.BeginStep("Selecting frequent items");
            var frequentItems = _frequentItemsSelector.SelectFrequentItems(representation.ItemInfos.Values, minimumSupport);
            progressTracker.EndStep();

            progressTracker.BeginStep("Sorting frequent items");
            var sortedFrequentItems = _sortingStrategy.Apply(frequentItems);
            progressTracker.EndStep();

            progressTracker.BeginStep("Building GRM tree");
            var root = _treeBuilder.Build(sortedFrequentItems, representation.DecisionIDs.Values, representation.TransactionDecisions);
            progressTracker.EndStep();

            progressTracker.BeginStep("Running GARM procedure");
            _garmProcedure.Execute(root, representation.TransactionDecisions, minimumSupport);
            progressTracker.EndStep();

            progressTracker.BeginStep("Building result");
            var decisionsGenerators = _decisionGeneratorsCollector.GetDecisionsGenerators();
            var result = _grmResultBuilder.GetResult(representation.AttributesCount, representation.DecisionAttributeIndex, representation.AttributeNames, representation.DecisionIDs, representation.ItemIDs, decisionsGenerators);
            progressTracker.EndStep();

            progressTracker.EndTask();
            return result;
        }
    }
}
