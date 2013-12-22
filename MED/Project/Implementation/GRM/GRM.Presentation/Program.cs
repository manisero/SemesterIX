using System;
using System.IO;
using GRM.Logic;

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

            new GRMFacade().ExecuteGRM(args[0], minimumSupport);
        }
    }
}
