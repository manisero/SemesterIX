using System;
using System.IO;
using GRM.Logic;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Presentation.ResultWriting;
using NDesk.Options;

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

            WriteGRMParameters(options);

            var progressInfo = new ProgressInfo(step => Console.WriteLine(step),
                                                (step, duration) => Console.WriteLine("Lasted {0}\n", duration));

            ProgressInfoContainer.CurrentProgressInfo = progressInfo;

            var dataSetStream = new FileStream(options.DataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var result = new GRMFacade(options.SortingStrategy, options.TransactionIdsStorageStrategy).ExecuteGRM(dataSetStream, options.DataFileContainsHeaders, options.DecisionAttributeIndex, options.MinimumSupport.Value);

            Console.WriteLine("GRM execution finished. Lasted {0}", progressInfo.GetOverallTaskDuration());
            Console.WriteLine("Step duration details:");

            foreach (var duration in progressInfo.GetSubstepsDurations())
            {
                Console.WriteLine("- {0}: {1}", duration.Key, duration.Value);
            }

            Console.WriteLine();
            
            WriteGRMResult(result, options.DataFilePath);
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

            shouldTerminate = false;
            return options;
        }

        private static void WriteGRMParameters(Options options)
        {
            Console.WriteLine("Executing GRM for file '{0}'.", options.DataFilePath);
            Console.WriteLine("The file is{0}expected to contain attribute names.", options.DataFileContainsHeaders ? " " : " not ");
            Console.WriteLine("Decision attribute: {0}.", options.DecisionAttributeIndex.HasValue ? (options.DecisionAttributeIndex + 1).ToString() : "last");
            Console.WriteLine("Minimum support: {0}.", options.MinimumSupport);
            Console.WriteLine("Sorting strategy: '{0}'.", options.SortingStrategy);
            Console.WriteLine("Transaction IDs storage strategy: '{0}'.", options.TransactionIdsStorageStrategy);
            Console.WriteLine();
        }

        private static void WriteGRMResult(GRMResult result, string dataFilePath)
        {
            var outputDirectoryName = Path.GetDirectoryName(dataFilePath);
            var dataFilename = Path.GetFileNameWithoutExtension(dataFilePath);

            var textOutputFileName = dataFilename + "_rules.txt";
            var textOutputFilePath = Path.Combine(outputDirectoryName, textOutputFileName);

            new TextResultWriter().WriteResult(result, textOutputFilePath);

            Console.WriteLine("Text result saved to {0}", textOutputFilePath);

            var csvOutputFileName = dataFilename + "_rules.csv";
            var csvOutputFilePath = Path.Combine(outputDirectoryName, csvOutputFileName);

            new CSVResultWriter().WriteResult(result, csvOutputFilePath);

            Console.WriteLine("CSV result saved to {0}", csvOutputFilePath);
        }
    }
}
