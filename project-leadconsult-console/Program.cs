using Serilog;
using System;

namespace project_leadconsult
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            Log.Logger = new LoggerConfiguration().WriteTo.File("log-.txt", rollingInterval: RollingInterval.Day).CreateLogger();
            Log.Information("Hello World!");

            // Finally, once just before the application exits...
            Log.CloseAndFlush();
        }
    }
}
