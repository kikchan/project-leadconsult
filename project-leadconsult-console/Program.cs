using project_leadconsult_core.BC;
using project_leadconsult_core.BE;
using Serilog;
using System;

namespace project_leadconsult
{
    /// <summary>
    /// Program
    /// </summary>
    internal class Program
    {
        #region Fields

        /// <summary>
        /// The coordinates bc
        /// </summary>
        private static readonly ICoordinatesBC coordinatesBC;

        #endregion Fields

        #region Constructor

        /// <summary>
        /// Prevents a default instance of the <see cref="Program" /> class from being created.
        /// </summary>
        static Program()
        {
            Log.Logger = new LoggerConfiguration()
            .WriteTo.File("log-.txt", outputTemplate: "{Timestamp:yyyy-MM-dd HH:mm:ss.fff} [{Level:u3}] {Message:lj}{NewLine}{Exception}", rollingInterval: RollingInterval.Day)
            .CreateLogger();

            coordinatesBC = new CoordinatesBC();
        }

        #endregion Constructor

        /// <summary>
        /// Defines the entry point of the application.
        /// </summary>
        /// <param name="args">The arguments.</param>
        private static void Main(string[] args)
        {
            Guid CorrelationID = Guid.NewGuid();

            try
            {
                if (args != null && args.Length == 2)
                {
                    if (System.IO.File.Exists(args[0]))
                    {
                        Console.WriteLine(args[0]);

                        ProcessFileRequest processFileRequest = new ProcessFileRequest(CorrelationID)
                        {
                            FileName = args[0],
                            Target = args[1]
                        };

                        ProcessFileResponse processFileResponse = coordinatesBC.ProcessFile(processFileRequest);

                        if (args[1].ToLower() == Literals.Console)
                        {
                            Console.WriteLine(Literals.FurthestPoints);

                            foreach (Point p in processFileResponse.FurthestPointsFromCenter)
                            {
                                Console.WriteLine($"\t- Point{p.No}({p.Coordinates.X},{p.Coordinates.Y}) in {p.GetQuadrant()}");
                            }
                        }
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
                    Console.WriteLine(Literals.ExampleOfUsage1);
                    Console.WriteLine(Literals.ExampleOfUsage2);
                    Log.Error(Literals.InvalidParameter);
                }
            }
            catch (Exception ex)
            {
                Log.Error(ex, Literals.Exception);
            }
            finally
            {
                Log.CloseAndFlush();
            }

            Console.WriteLine(Literals.PressAnyKeyToExit);
            //Console.ReadKey();
        }
    }
}