using System;
using System.IO;
using GRM.Logic;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.GRMAlgorithm.Entities;

namespace GRM.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new ArgsParser().ParseArgs(args);

            Console.WriteLine("Executing GRM for file '{0}' with minimum support = {1}, sorting strategy = '{2}', and transaction ids storage strategy = '{3}'",
                              options.DataFilePath, options.MinimumSupport, options.SortingStrategy, options.TransactionIdsStorageStrategy);
            Console.WriteLine();

            var dataSetStream = new FileStream(options.DataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var progressInfo = new ProgressInfo(step => Console.WriteLine(step),
                                                (step, duration) => Console.WriteLine("Lasted {0}\n", duration));
            
            var result = new GRMFacade(options.SortingStrategy, options.TransactionIdsStorageStrategy).ExecuteGRM(dataSetStream, options.MinimumSupport.Value, progressInfo);
            Console.WriteLine("GRM execution finished. Lasted {0}", progressInfo.GetOverallTaskDuration());

            var outputFilePath = WriteGRMResult(result, options.DataFilePath);
            Console.WriteLine("Result saved to {0}", outputFilePath);
        }

        private static string WriteGRMResult(GRMResult result, string dataFilePath)
        {
            var outputDirectoryName = Path.GetDirectoryName(dataFilePath);
            var outputFileName = Path.GetFileNameWithoutExtension(dataFilePath) + "_rules.txt";
            var outputFilePath = Path.Combine(outputDirectoryName, outputFileName);

            new GRMResultWriter().WriteResult(result, outputFilePath);

            return outputFilePath;
        }
    }
}
