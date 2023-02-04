using project_leadconsult_core.BC;
using project_leadconsult_core.Utils;
using Serilog;
using System;

namespace project_leadconsult
{
    /// <summary>
    /// Program
    /// </summary>
    internal class Program
    {
        /// <summary>
        /// The coordinates bc
        /// </summary>
        static readonly ICoordinatesBC coordinatesBC;

        /// <summary>
        /// Prevents a default instance of the <see cref="Program"/> class from being created.
        /// </summary>
        static Program()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();

            coordinatesBC = new CoordinatesBC();
        }

        private static void Main(string[] args)
        {
            if (args != null && args.Length == 1)
            {
                if (System.IO.File.Exists(args[0]))
                {
                    Console.WriteLine(args[0]);
                    Log.Information(args[0]);

                    coordinatesBC.ProcessFile(args[0]);

                    //Literals.Point
                }
                else
                {
                    Console.WriteLine(Literals.FileNotExists);
                    Log.Error(Literals.FileNotExists);
                }
            }
            else
            {
                Console.WriteLine(Literals.InvalidParameter);
                Log.Error(Literals.InvalidParameter);
            }

            // Finally, once just before the application exits...
            Log.CloseAndFlush();

            Console.WriteLine(Literals.PressAnyKeyToExit);
            Console.ReadKey();
        }
    }
}