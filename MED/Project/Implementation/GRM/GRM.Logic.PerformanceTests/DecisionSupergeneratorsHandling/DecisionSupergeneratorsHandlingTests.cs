﻿using System;
using System.IO;
using System.Text;
using GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting;
using GRM.Logic.GRMAlgorithm.ItemsSorting;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage;
using GRM.Logic.ProgressTracking;
using GRM.Logic.ProgressTracking.ProgressTrackers;
using System.Linq;
using Xunit;

namespace GRM.Logic.PerformanceTests.DecisionSupergeneratorsHandling
{
    public class DecisionSupergeneratorsHandlingTests
    {
        private void Execute(DecisionSupergeneratorsHandlingStrategyType decisionSupergeneratorsHandlingStrategyType)
        {
            // Arrange
            ProgressTrackerContainer.CurrentProgressTracker = new SubstepProgressTracker();

            // Act
            using (var dataSetStream = new MemoryStream(ASCIIEncoding.Default.GetBytes(Resources.Resources.mushrooms)))
            {
                new Logic.GRMFacade(SortingStrategyType.DescendingSupport, TransactionIDsStorageStrategyType.DiffSets, decisionSupergeneratorsHandlingStrategyType).ExecuteGRM(dataSetStream, true, 0, 100);
            }

            // Assert
            var taskInfo = ProgressTrackerContainer.CurrentProgressTracker.GetInfo();
            var removalDuration = taskInfo.Steps.Single(x => x.Name == "Running GARM procedure")
                                          .Substeps.Single(x => x.Name == "Updating decision generators")
                                          .TotalDuration;

            Console.WriteLine("GRM took {0}", taskInfo.Duration);
            Console.WriteLine("Supergenerators removal took {0}", removalDuration);
        }

        [Fact]
        public void brute_force_test()
        {
            Execute(DecisionSupergeneratorsHandlingStrategyType.BruteForce);
        }

        [Fact]
        public void inverted_lists_test()
        {
            Execute(DecisionSupergeneratorsHandlingStrategyType.InvertedLists);
        }
    }
}
