using Serilog;
using System;

namespace project_leadconsult
{
    /// <summary>
    /// Program
    /// </summary>
    class Program
    {

        /// <summary>
        /// Prevents a default instance of the <see cref="Program"/> class from being created.
        /// </summary>
        Program()
        {
            Log.Logger = new LoggerConfiguration().WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            if (args != null && args.Length == 1)
            {
                if (System.IO.File.Exists(args[0]))
                {
                    Log.Information("Hello World!");
                }
                else
                {
                    Console.WriteLine("The given file doesn't exist");
                    Log.Error("The given file doesn't exist");
                }
            }
            else
            {
                Console.WriteLine("You must pass by parameter the name of the text file that you want to process");
                Log.Error("You must pass by parameter the name of the text file that you want to process");
            }

            // Finally, once just before the application exits...
            Log.CloseAndFlush();

            Console.WriteLine("\n\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}
