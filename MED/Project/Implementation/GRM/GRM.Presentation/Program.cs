using System;
using System.IO;
using GRM.Logic;
using GRM.Logic.DataSetProcessing;

namespace GRM.Presentation
{
    class Program
    {
        static void Main(string[] args)
        {
            int minimumSupport;

            if (args.Length != 2 || !int.TryParse(args[1], out minimumSupport))
            {
                Console.WriteLine("Usage:");

                var applicationPath = Environment.GetCommandLineArgs()[0];
                Console.WriteLine("{0} <data file path> <minimum support [integer]>", Path.GetFileName(applicationPath));
                return;
            }

            var progressInfo = new ProgressInfo(step => Console.WriteLine(step),
                                                (step, duration) => Console.WriteLine("Lasted {0}\n", duration));

            new GRMFacade().ExecuteGRM(args[0], minimumSupport, progressInfo);

            Console.WriteLine("GRM execution finished. Lasted {0}", progressInfo.GetOverallTaskDuration());
        }
    }
}
