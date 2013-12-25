using System.IO;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.DataSetProcessing._Impl;
using GRM.Logic.GRMAlgorithm;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm._Impl;

namespace GRM.Logic
{
    public class GRMFacade
    {
        private readonly IDataSetRepresentationBuilder _dataSetRepresentationBuilder;
        private readonly IFrequentItemsSelector _frequentItemsSelector;
        private readonly ITreeBuilder _treeBuilder;
        private readonly IResultBuilder _resultBuilder;
        private readonly IGARMProcedure _garmProcedure;

        public GRMFacade()
        {
            _dataSetRepresentationBuilder = new DataSetRepresentationBuilder(new TransactionProcessor());
            _frequentItemsSelector = new FrequentItemsSelector();
            _treeBuilder = new TreeBuilder();
            _resultBuilder = new ResultBuilder();
            _garmProcedure = new GARMProcedure(_resultBuilder, new GARMPropertyProcedure());
        }

        public GRMResult ExecuteGRM(string dataFilePath, int minimumSupport, ProgressInfo progressInfo)
        {
            progressInfo.BeginTask();

            var stream = new FileStream(dataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);

            progressInfo.BeginStep("Creating data set representation");
            var representation = _dataSetRepresentationBuilder.Build(stream);
            progressInfo.EndStep();

            progressInfo.BeginStep("Selecting frequent items");
            var frequentItems = _frequentItemsSelector.SelectFrequentItems(representation.ItemInfos.Values, minimumSupport);
            progressInfo.EndStep();

            progressInfo.BeginStep("Building GRM tree");
            var root = _treeBuilder.Build(frequentItems, representation.DecisionIDs.Values, representation.TransactionDecisions);
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
