﻿using project_leadconsult_core.BC;
using project_leadconsult_core.BE;
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
                // Log input
                SerilogLogger.TraceLogIn(CorrelationID, args);

                if (args != null && args.Length == 2)
                {
                    if (System.IO.File.Exists(args[0]))
                    {
                        Console.WriteLine(string.Concat(Literals.Processing, ": ", args[0]));

                        ProcessFileRequest processFileRequest = new ProcessFileRequest(CorrelationID)
                        {
                            FileName = args[0],
                            Target = args[1]
                        };

                        ProcessFileResponse processFileResponse = coordinatesBC.ProcessFile(processFileRequest);

                        if (processFileResponse.Response == project_leadconsult_core.Enums.ResponseStatuses.OK)
                        {
                            if (args[1].ToLower() == Literals.Console)
                            {
                                Console.WriteLine(Literals.FurthestPoints);

                                foreach (Point p in processFileResponse.FurthestPointsFromCenter)
                                {
                                    Console.WriteLine($"\t- Point{p.No}({p.Coordinates.X},{p.Coordinates.Y}) in {p.GetQuadrant()}");
                                }
                            }
                            else
                            {
                                Console.Write(Literals.GeneratedFile);
                                Console.WriteLine(processFileRequest.Target);
                            }
                        }

                        // Log output
                        SerilogLogger.TraceLogOut(CorrelationID, processFileResponse.Response);
                    }
                    else
                    {
                        Console.WriteLine(Literals.FileNotExists);

                        // Log error
                        SerilogLogger.TraceError(CorrelationID, Literals.FileNotExists);
                    }
                }
                else
                {
                    Console.WriteLine(Literals.InvalidParameter);
                    Console.WriteLine(Literals.ExampleOfUsage1);
                    Console.WriteLine(Literals.ExampleOfUsage2);

                    // Log error
                    SerilogLogger.TraceError(CorrelationID, Literals.InvalidParameter);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(Literals.Exception);

                // Trace catch
                SerilogLogger.TraceCatch(CorrelationID, ex);
            }
            finally
            {
                Log.CloseAndFlush();
            }

            Console.WriteLine(Literals.PressAnyKeyToExit);
            Console.ReadKey();
        }
    }
}