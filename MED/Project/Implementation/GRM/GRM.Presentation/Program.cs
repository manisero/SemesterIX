using System;
using System.IO;
using GRM.Logic;
using GRM.Logic.DataSetProcessing;
using GRM.Logic.GRMAlgorithm.Entities;
using GRM.Logic.GRMAlgorithm.ItemsSorting;

namespace GRM.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            int minimumSupport;
            SortingStrategyType sortingStrategy;

            if (args.Length < 2 || !int.TryParse(args[1], out minimumSupport) || !TryGetSortingStrategy(args, out sortingStrategy))
            {
                Console.WriteLine("Usage:");

                var applicationPath = Environment.GetCommandLineArgs()[0];
                Console.WriteLine("{0} <data file path> <minimum support [integer]> <sorting strategy [0-3]>", Path.GetFileName(applicationPath));
                return;
            }

            var dataFilePath = args[0];

            var progressInfo = new ProgressInfo(step => Console.WriteLine(step),
                                                (step, duration) => Console.WriteLine("Lasted {0}\n", duration));

            Console.WriteLine("Executing GRM for file '{0}' with minimum support = {1} and sorting strategy = '{2}'", dataFilePath, minimumSupport, sortingStrategy);
            Console.WriteLine();

            var dataSetStream = new FileStream(dataFilePath, FileMode.Open, FileAccess.Read, FileShare.Read);
            var result = new GRMFacade().ExecuteGRM(dataSetStream, minimumSupport, sortingStrategy, progressInfo);
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
