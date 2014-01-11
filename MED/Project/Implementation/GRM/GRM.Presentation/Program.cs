using System;
using System.IO;
using GRM.Logic;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Presentation.ResultWriting;

namespace GRM.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new ArgsParser().ParseArgs(args);

            WriteGRMParameters(options);

            var dataSetStream = new FileStream(options.DataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var progressInfo = new ProgressInfo(step => Console.WriteLine(step),
                                                (step, duration) => Console.WriteLine("Lasted {0}\n", duration));

            var result = new GRMFacade(options.SortingStrategy, options.TransactionIdsStorageStrategy).ExecuteGRM(dataSetStream, options.DataFileContainsHeaders, options.DecisionAttributeIndex,
                                                                                                                  options.MinimumSupport.Value, progressInfo);

            Console.WriteLine("GRM execution finished. Lasted {0}", progressInfo.GetOverallTaskDuration());

            WriteGRMResult(result, options.DataFilePath);
        }

        private static void WriteGRMParameters(Options options)
        {
            Console.WriteLine("Executing GRM for file '{0}'.", options.DataFilePath);

            if (options.DataFileContainsHeaders)
            {
                Console.WriteLine("The file is expected to contain attributes names.");
            }

            Console.WriteLine("Decision attribute: {0}.", options.DecisionAttributeIndex.HasValue ? options.DecisionAttributeIndex.ToString() : "last");
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
        }
    }
}
