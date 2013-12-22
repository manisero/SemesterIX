using System;
using System.IO;
using GRM.Logic.DataSetProcessing._Impl;

namespace GRM.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Usage:");

                var applicationPath = Environment.GetCommandLineArgs()[0];
                Console.WriteLine("{0} <data file path>", Path.GetFileName(applicationPath));
                return;
            }

            var stream = new FileStream(args[0], FileMode.Open, FileAccess.Read, FileShare.Read);

            var representation = new DataSetRepresentationBuilder(new TransactionProcessor()).Build(stream);
        }
    }
}
