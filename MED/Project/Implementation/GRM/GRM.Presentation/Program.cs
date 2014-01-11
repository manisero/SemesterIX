using System;
using System.IO;
using GRM.Logic;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm.ItemsSorting;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage;

namespace GRM.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            var options = new ArgsParser().ParseArgs(args);

            int minimumSupport;
            SortingStrategyType sortingStrategy;
            TransactionIDsStorageStrategyType transactionIdsStorageStrategy;

            if (args.Length < 2 || !int.TryParse(args[1], out minimumSupport) || !TryGetSortingStrategy(args, out sortingStrategy) || !TryGetTransactionIDsStorageStrategy(args, out transactionIdsStorageStrategy))
            {
                Console.WriteLine("Usage:");

                var applicationPath = Environment.GetCommandLineArgs()[0];
                Console.WriteLine("{0} <data file path> <minimum support [integer]> <sorting strategy [0-3]> <transaction IDs storage strategy [TIDSets|DiffSets]>", Path.GetFileName(applicationPath));
                return;
            }

            var dataFilePath = args[0];

            var progressInfo = new ProgressInfo(step => Console.WriteLine(step),
                                                (step, duration) => Console.WriteLine("Lasted {0}\n", duration));

            Console.WriteLine("Executing GRM for file '{0}' with minimum support = {1} and sorting strategy = '{2}'", dataFilePath, minimumSupport, sortingStrategy);
            Console.WriteLine();

            var dataSetStream = new FileStream(dataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var result = new GRMFacade(sortingStrategy, transactionIdsStorageStrategy).ExecuteGRM(dataSetStream, minimumSupport, progressInfo);
            Console.WriteLine("GRM execution finished. Lasted {0}", progressInfo.GetOverallTaskDuration());

            var outputFilePath = WriteGRMResult(result, dataFilePath);
            Console.WriteLine("Result saved to {0}", outputFilePath);
        }

        private static bool TryGetSortingStrategy(string[] args, out SortingStrategyType result)
        {
            if (args.Length < 3)
            {
                result = 0;
                return true;
            }

            int strategyType;

            if (int.TryParse(args[2], out strategyType))
            {
                var possibleValues = Enum.GetValues(typeof(SortingStrategyType));

                if (strategyType >= 0 && strategyType < possibleValues.Length)
                {
                    result = (SortingStrategyType)strategyType;
                    return true;
                }
            }

            result = 0;
            return false;
        }

        private static bool TryGetTransactionIDsStorageStrategy(string[] args, out TransactionIDsStorageStrategyType result)
        {
            if (args.Length < 4)
            {
                result = 0;
                return true;
            }

            try
            {
                result = (TransactionIDsStorageStrategyType)Enum.Parse(typeof(TransactionIDsStorageStrategyType), args[3], true);
                return true;
            }
            catch
            {
                // Do notning, return false eventually
            }
            
            result = 0;
            return false;
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
