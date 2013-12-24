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

        public GRMFacade()
        {
            _dataSetRepresentationBuilder = new DataSetRepresentationBuilder(new TransactionProcessor());
            _frequentItemsSelector = new FrequentItemsSelector();
            _treeBuilder = new TreeBuilder();
            _resultBuilder = new ResultBuilder();
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
            var tree = _treeBuilder.Build(frequentItems, representation.DecisionIDs.Values, representation.TransactionDecisions);
            progressInfo.EndStep();

            if (_resultBuilder.CanBuildResult(tree.RuleGenerators))
            {
                return _resultBuilder.Build(tree.RuleGenerators, representation.DecisionIDs, representation.ItemIDs);
            }

            progressInfo.EndTask();
            return null;
        }
    }
}
