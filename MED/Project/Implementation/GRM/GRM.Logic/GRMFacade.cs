﻿using System.IO;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.DataSetProcessing._Impl;
using GRM.Logic.GRMAlgorithm;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm.ItemsSorting;
using GRM.Logic.GRMAlgorithm.ItemsSorting._Impl;
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
        private readonly IResultBuilder _resultBuilder;
        private readonly IGARMProcedure _garmProcedure;

        public GRMFacade(SortingStrategyType sortingStrategy, TransactionIDsStorageStrategyType transactionIdsStorageStrategy)
        {
            _dataSetRepresentationBuilder = new DataSetRepresentationBuilder(new TransactionProcessor());
            _frequentItemsSelector = new FrequentItemsSelector();
            _sortingStrategy = new SortingStrategyFactory().Create(sortingStrategy);

            var storageStrategy = new TransactionIDsStorageStrategyFactory().Create(transactionIdsStorageStrategy);
            _treeBuilder = new TreeBuilder(storageStrategy);
            _resultBuilder = new ResultBuilder();
            _garmProcedure = new GARMProcedure(_resultBuilder, new GARMPropertyProcedure(storageStrategy));
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
            var result = _resultBuilder.GetResult(representation.AttributesCount, representation.DecisionAttributeIndex, representation.AttributeNames, representation.DecisionIDs, representation.ItemIDs);
            progressTracker.EndStep();

            progressTracker.EndTask();
            return result;
        }
    }
}
