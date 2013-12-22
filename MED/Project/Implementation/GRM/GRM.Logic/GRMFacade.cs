using System.Collections.Generic;
using System.IO;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.DataSetProcessing.Entities;
using GRM.Logic.DataSetProcessing._Impl;
using GRM.Logic.GRMAlgorithm;
using GRM.Logic.GRMAlgorithm._Impl;
using System.Linq;

namespace GRM.Logic
{
    public class GRMFacade
    {
        private readonly IDataSetRepresentationBuilder _dataSetRepresentationBuilder;
        private readonly IFrequentItemsSelector _frequentItemsSelector;
        private readonly ITreeBuilder _treeBuilder;

        public GRMFacade()
        {
            _dataSetRepresentationBuilder = new DataSetRepresentationBuilder(new TransactionProcessor());
            _frequentItemsSelector = new FrequentItemsSelector();
            _treeBuilder = new TreeBuilder();
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

            GRMResult result;

            if (TryBuildResult(tree.Generators, representation.DecisionIDs, representation.ItemIDs, out result))
            {
                return result;
            }

            progressInfo.EndTask();
            return result;
        }

        private bool TryBuildResult(IDictionary<int, IEnumerable<ItemID>> generators, IDictionary<string, int> decisionIds, IDictionary<Item, ItemID> itemIds,
                                    out GRMResult result)
        {
            if (generators.Any(x => x.Value == null))
            {
                result = null;
                return false;
            }

            var rules = new List<Rule>();

            foreach (var generator in generators)
            {
                var items = new List<Item>();

                foreach (var itemId in generator.Value)
                {
                    items.Add(itemIds.Single(x => x.Value.Equals(itemId)).Key);
                }

                rules.Add(new Rule
                    {
                        Items = items,
                        Decision = decisionIds.Single(x => x.Value == generator.Key).Key
                    });
            }

            result = new GRMResult { Rules = rules };
            return true;
        }
    }
}
