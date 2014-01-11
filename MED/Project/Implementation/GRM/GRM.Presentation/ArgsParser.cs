using System;
using GRM.Logic.GRMAlgorithm.ItemsSorting;
using GRM.Logic.GRMAlgorithm.TransactionIDsStorage;
using NDesk.Options;

namespace GRM.Presentation
{
    public class ArgsParser
    {
        public Options ParseArgs(string[] args)
        {
            var options = new Options();
            var optionSet = BuildOptionSet(options);

            try
            {
                optionSet.Parse(args);

                if (options.DataFilePath == null)
                {
                    throw new OptionException("file parameter is required", "file");
                }

                if (options.MinimumSupport == null)
                {
                    throw new OptionException("minSup parameter is required", "minSup");
                }
            }
            catch (OptionException e)
            {
                Console.WriteLine(e);
                Console.WriteLine();
                Console.WriteLine("Parameters:");
                optionSet.WriteOptionDescriptions(Console.Out);
            }

            return options;
        }

        private OptionSet BuildOptionSet(Options options)
        {
            var optionSet = new OptionSet();
            optionSet.Add("f|file=", "Data file path. Required.", x => options.DataFilePath = x);
            optionSet.Add("sup|minSup=", "Minimum support. Required.", (int x) => options.MinimumSupport = x);
            optionSet.Add("sort=", "Sorting strategy. Optional. Valid values: DescendingSupport (or 0; default), AscendingSupport (or 1), Lexicographical (or 2), ReverseLexicographical (or 3).", x => options.SortingStrategy = ParseEnum<SortingStrategyType>(x));
            optionSet.Add("store=", "Transaction IDs storage strategy. Optional. Valid values: TIDSets (or 0; default), DiffSets (or 1).", x => options.TransactionIdsStorageStrategy = ParseEnum<TransactionIDsStorageStrategyType>(x));

            return optionSet;
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
