using System;
using GRM.Logic.GRMAlgorithm.DecisionGeneratorsCollecting;
using GRM.Logic.GRMAlgorithm.ItemsSorting;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage;
using GRM.Logic.ProgressTracking;
using NDesk.Options;

namespace GRM.Presentation
{
    public class ArgsParser
    {
        public OptionSet BuildOptionSet(Options options)
        {
            var optionSet = new OptionSet();
            optionSet.Add("help", "Makes the program display available parameters and terminate. Optional.", x => options.HelpRequested = true);
            optionSet.Add("f|file=", "Data file path. Required.", x => options.DataFilePath = x);
            optionSet.Add("sup|minSup=", "Minimum support. Required.", (int x) => options.MinimumSupport = x);
            optionSet.Add("h|headers", "Indicates that the first row of data file contains attribute names (headers). Optional.", x => options.DataFileContainsHeaders = true);
            optionSet.Add("dec|decAttr=", "Decision attribute index (1 = first attribute, 2 = second attribute...). Optional (if not provided, last attribute is considered as decision).", (int x) => options.DecisionAttributeIndex = x - 1);
            optionSet.Add("sort=", "Sorting strategy. Optional. Valid values: AscendingSupport (or 0; default), DescendingSupport (or 1), Lexicographical (or 2).", x => options.SortingStrategy = ParseEnum<SortingStrategyType>(x));
            optionSet.Add("store=", "Transaction IDs storage strategy. Optional. Valid values: TIDSets (or 0; default), DiffSets (or 1).", x => options.TransactionIdsStorageStrategy = ParseEnum<TransactionIDsStorageStrategyType>(x));
            optionSet.Add("supgen=", "Decision supergenerators handling strategy. Optional. Valid values: InvertedLists (or 0; default), BruteForce (or 1).", x => options.DecisionSupergeneratorsHandlingStrategy = ParseEnum<DecisionSupergeneratorsHandlingStrategyType>(x));
            optionSet.Add("track=", "Performance tracking level. Optional. Valid values: NoTracking (or 0), Task (or 1; default), Steps (or 2), Substeps (or 3; CAUTION: increases overall execution time significantly).", x => options.TrackingLevel = ParseEnum<TrackingLevel>(x));
            optionSet.Add("o|output=", "Output files path. Optional. Valid value is a file path without file extension (e.g. results/result). Default value: [data file path]_rules.", x => options.OutputPath = x);
            
            return optionSet;
        }

        public void ParseArgs(string[] args, OptionSet optionSet, Options options)
        {
            optionSet.Parse(args);

            if (options.HelpRequested)
            {
                return;
            }

            if (options.DataFilePath == null)
            {
                throw new OptionException("file parameter is required", "file");
            }

            if (options.MinimumSupport == null)
            {
                throw new OptionException("minSup parameter is required", "minSup");
            }
        }

        public void PrintParameters(OptionSet optionSet)
        {
            Console.WriteLine("Parameters:");
            optionSet.WriteOptionDescriptions(Console.Out);
        }

        private TEnum ParseEnum<TEnum>(string value) where TEnum:struct 
        {
            try
            {
                int strategyId;

                if (int.TryParse(value, out strategyId))
                {
                    return (TEnum)Enum.GetValues(typeof(TEnum)).GetValue(strategyId);
                }

                return (TEnum)Enum.Parse(typeof(TEnum), value, true);
            }
            catch (Exception)
            {
                return (TEnum)Enum.GetValues(typeof(TEnum)).GetValue(0);
            }
        }
    }
}
