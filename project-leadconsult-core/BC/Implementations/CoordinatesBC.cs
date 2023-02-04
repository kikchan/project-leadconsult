using project_leadconsult_core.BE;
using project_leadconsult_core.Utils;
using Serilog;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.RegularExpressions;

namespace project_leadconsult_core.BC
{
    /// <summary>
    /// CoordinatesBC
    /// </summary>
    /// <seealso cref="project_leadconsult_core.BC.ICoordinatesBC" />
    public class CoordinatesBC : ICoordinatesBC
    {
        /// <summary>
        /// The center
        /// </summary>
        private readonly Point center = new Point(0, 0, 0);

        /// <summary>
        /// Processes the file.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        public ProcessFileResponse ProcessFile(ProcessFileRequest request)
        {
            ProcessFileResponse response = new ProcessFileResponse();

            try
            {
                // Log input
                SerilogLogger.TraceLogIn(request.CorrelationID, request);

                if (request != null)
                {
                    List<Point> points = ReadFile(request.FileName);
                    List<Point> furthestPointsFromCenter = CalculateFurthestPointsFromCenter(points);

                    if (furthestPointsFromCenter != null && furthestPointsFromCenter.Count > 0)
                    {
                        response.Response = Enums.ResponseStatuses.OK;
                        response.FurthestPointsFromCenter = furthestPointsFromCenter;

                        if (request.Target.ToLower() != Literals.Console)
                        {
                            using (TextWriter tw = new StreamWriter(request.Target))
                            {
                                foreach (Point p in response.FurthestPointsFromCenter)
                                {
                                    tw.WriteLine($"Point{p.No}({p.Coordinates.X},{p.Coordinates.Y}) in {p.GetQuadrant()}");
                                }
                            }
                        }
                    }
                    else
                    {
                        response.Response = Enums.ResponseStatuses.NoDataFound;
                        response.ResponseMessage = Enums.ResponseStatuses.NoDataFound.ToString();
                    }
                }
                else
                {
                    response.Response = Enums.ResponseStatuses.NullRequest;
                    response.ResponseMessage = Enums.ResponseStatuses.NullRequest.ToString();
                }
            }
            catch (Exception ex)
            {
                response.Response = Enums.ResponseStatuses.Exception;
                response.ResponseMessage = ex.Message;

                // Trace catch
                SerilogLogger.TraceCatch(request.CorrelationID, ex);
            }

            // Log output
            SerilogLogger.TraceLogOut(request.CorrelationID, response.Response);

            return response;
        }

        /// <summary>
        /// Calculates the furthest points from center.
        /// </summary>
        /// <param name="points">The points.</param>
        /// <returns></returns>
        private List<Point> CalculateFurthestPointsFromCenter(List<Point> points)
        {
            List<PointDistance> results = new List<PointDistance>();

            foreach (Point p in points)
            {
                PointDistance pd = new PointDistance()
                {
                    Distance = Helper.EuclideanPlane(center, p),
                    Point = p
                };

                results.Add(pd);
            }

            results = results.OrderByDescending(x => x.Distance).ToList();

            if (results.Count > 1)
            {
                int takeNElements = 1;

                for (int i = 0; i < results.Count; i++)
                {
                    if (results[i].Distance == results[i + 1].Distance)
                    {
                        takeNElements++;
                    }
                    else
                    {
                        break;
                    }
                }

                results = results.Take(takeNElements).OrderBy(x => x.Point.No).ToList();
            }

            return results.Select(x => x.Point).ToList();
        }

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        /// <returns></returns>
        private List<Point> ReadFile(string filename)
        {
            List<Point> points = new List<Point>();

            using (StreamReader sr = File.OpenText(filename))
            {
                string[] lineValues = sr.ReadToEnd().Split(Environment.NewLine);

                foreach (string line in lineValues)
                {
                    string purifiedLine = Regex.Replace(line, "[^0-9,()-]", string.Empty);

                    bool parsedNo = int.TryParse(purifiedLine.Substring(0, purifiedLine.IndexOf("(")), out int no);
                    bool parsedX = int.TryParse(purifiedLine.Substring(purifiedLine.IndexOf("(") + 1, purifiedLine.IndexOf(",") - purifiedLine.IndexOf("(") - 1), out int x);
                    bool parsedY = int.TryParse(purifiedLine.Substring(purifiedLine.IndexOf(",") + 1, purifiedLine.IndexOf(")") - purifiedLine.IndexOf(",") - 1), out int y);

                    if (parsedNo && parsedX && parsedY)
                    {
                        points.Add(new Point(no, x, y));
                    }
                    else
                    {
                        Log.Information(string.Concat(Literals.CantParse), line);
                    }
                }
            }

            return points;
        }
    }
}