using System.IO;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.DataSetProcessing._Impl;
using GRM.Logic.GRMAlgorithm;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm.ItemsSorting;
using GRM.Logic.GRMAlgorithm.ItemsSorting._Impl;
using GRM.Logic.GRMAlgorithm._Impl;

namespace GRM.Logic
{
    public class GRMFacade
    {
        private readonly IDataSetRepresentationBuilder _dataSetRepresentationBuilder;
        private readonly IFrequentItemsSelector _frequentItemsSelector;
        private readonly ISortingStrategyFactory _sortingStrategyFactory;
        private readonly ITreeBuilder _treeBuilder;
        private readonly IResultBuilder _resultBuilder;
        private readonly IGARMProcedure _garmProcedure;

        public GRMFacade()
        {
            _dataSetRepresentationBuilder = new DataSetRepresentationBuilder(new TransactionProcessor());
            _frequentItemsSelector = new FrequentItemsSelector();
            _sortingStrategyFactory = new SortingStrategyFactory();
            _treeBuilder = new TreeBuilder();
            _resultBuilder = new ResultBuilder();
            _garmProcedure = new GARMProcedure(_resultBuilder, new GARMPropertyProcedure());
        }

        public GRMResult ExecuteGRM(Stream dataSetStream, int minimumSupport, SortingStrategyType sortingStrategy, ProgressInfo progressInfo)
        {
            progressInfo.BeginTask();

            progressInfo.BeginStep("Creating data set representation");
            var representation = _dataSetRepresentationBuilder.Build(dataSetStream);
            progressInfo.EndStep();

            progressInfo.BeginStep("Selecting frequent items");
            var frequentItems = _frequentItemsSelector.SelectFrequentItems(representation.ItemInfos.Values, minimumSupport);
            progressInfo.EndStep();

            progressInfo.BeginStep("Sorting frequent items");
            var sortedFrequentItems = _sortingStrategyFactory.Create(sortingStrategy).Apply(frequentItems);
            progressInfo.EndStep();

            progressInfo.BeginStep("Building GRM tree");
            var root = _treeBuilder.Build(sortedFrequentItems, representation.DecisionIDs.Values, representation.TransactionDecisions);
            progressInfo.EndStep();

            progressInfo.BeginStep("Running GARM procedure");
            _garmProcedure.Execute(root, representation.TransactionDecisions, minimumSupport);
            progressInfo.EndStep();

            progressInfo.BeginStep("Building result");
            var result = _resultBuilder.GetResult(representation.DecisionIDs, representation.ItemIDs);
            progressInfo.EndStep();

            progressInfo.EndTask();
            return result;
        }
    }
}
