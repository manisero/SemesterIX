using System;
using System.IO;
using GRM.Logic;
using GRM.Logic.ProgressTracking;
using GRM.Logic.ProgressTracking._Impl;
using GRM.Presentation.ResultWriting;
using NDesk.Options;
using System.Linq;

namespace GRM.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            bool shouldTerminate;
            var options = ReadArgs(args, out shouldTerminate);

            if (shouldTerminate)
            {
                return;
            }

            PrintGRMParameters(options);

            ProgressTrackerContainer.CurrentProgressTracker = new ProgressTrackerFactory().Create(options.TrackingLevel);

            try
            {
                var dataSetStream = new FileStream(options.DataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
                var result = new GRMFacade(options.SortingStrategy, options.TransactionIdsStorageStrategy, options.DecisionSupergeneratorsHandlingStrategy)
                                    .ExecuteGRM(dataSetStream, options.DataFileContainsHeaders, options.DecisionAttributeIndex, options.MinimumSupport.Value);

                Console.WriteLine("GRM execution finished");
                PrintPerformanceInfo();
                Console.WriteLine();

                WriteGRMResult(result, options.OutputPath);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        private static Options ReadArgs(string[] args, out bool shouldTerminate)
        {
            var options = new Options();

            var argsParser = new ArgsParser();
            var optionSet = argsParser.BuildOptionSet(options);

            try
            {
                argsParser.ParseArgs(args, optionSet, options);
            }
            catch (OptionException e)
            {
                Console.WriteLine(e);
                Console.WriteLine();

                argsParser.PrintParameters(optionSet);

                shouldTerminate = true;
                return null;
            }

            if (options.HelpRequested)
            {
                argsParser.PrintParameters(optionSet);

                shouldTerminate = true;
                return null;
            }

            if (options.OutputPath == null)
            {
                options.OutputPath = GetDefaultOutputPath(options.DataFilePath);
            }

            shouldTerminate = false;
            return options;
        }

        private static string GetDefaultOutputPath(string dataFilePath)
        {
            var outputDirectory = Path.GetDirectoryName(dataFilePath);
            var outputFilePrefix = Path.GetFileNameWithoutExtension(dataFilePath);

            return Path.Combine(outputDirectory, outputFilePrefix);
        }

        private static void PrintGRMParameters(Options options)
        {
            Console.WriteLine("Executing GRM for file '{0}'", options.DataFilePath);
            Console.WriteLine("The file is{0}expected to contain attribute names", options.DataFileContainsHeaders ? " " : " not ");
            Console.WriteLine("Decision attribute: {0}", options.DecisionAttributeIndex.HasValue ? (options.DecisionAttributeIndex + 1).ToString() : "last");
            Console.WriteLine("Minimum support: {0}", options.MinimumSupport);
            Console.WriteLine("Sorting strategy: '{0}'", options.SortingStrategy);
            Console.WriteLine("Transaction IDs storage strategy: '{0}'", options.TransactionIdsStorageStrategy);
            Console.WriteLine("Decision supergenerators handling strategy: '{0}'", options.DecisionSupergeneratorsHandlingStrategy);
            Console.WriteLine("Performance tracking level: '{0}'", options.TrackingLevel);
            Console.WriteLine("Output files will be saved to: '{0}'", options.OutputPath);
            Console.WriteLine();
        }

        private static void PrintPerformanceInfo()
        {
            var taskInfo = ProgressTrackerContainer.CurrentProgressTracker.GetInfo();

            if (taskInfo == null)
            {
                return;
            }

            Console.WriteLine("Lasted {0}", taskInfo.Duration);

            if (taskInfo.Steps != null)
            {
                Console.WriteLine("Steps details:");

                var stepNumber = 1;

                foreach (var step in taskInfo.Steps)
                {
                    Console.WriteLine("{0}. {1}: {2}", stepNumber, step.Name, step.Duration);

                    foreach (var substep in step.Substeps)
                    {
                        Console.WriteLine("\t- {0}: {1} ({2} iterations)", substep.Name, substep.TotalDuration, substep.EntersCount);
                    }

                    stepNumber++;
                }
            }
        }

        private static void WriteGRMResult(GRMResult result, string outputPath)
        {
            var textOutputFilePath = outputPath + "_rules.txt";
            new TextResultWriter().WriteResult(result, textOutputFilePath);
            Console.WriteLine("Text result saved to {0}", textOutputFilePath);

            var csvOutputFilePath = outputPath + "_rules.csv";
            new CSVResultWriter().WriteResult(result, csvOutputFilePath);
            Console.WriteLine("CSV result saved to {0}", csvOutputFilePath);
        }
    }
}
