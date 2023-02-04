using project_leadconsult_core.BE;
using project_leadconsult_core.Utils;
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

            if (request != null)
            {
                try
                {
                    // Log input
                    SerilogLogger.TraceLogIn(request.CorrelationID, request);

                    OpenFileRequest openFileRequest = new OpenFileRequest(request.CorrelationID)
                    {
                        FileName = request.FileName
                    };

                    OpenFileResponse openFileResponse = ReadFile(openFileRequest);

                    if (openFileResponse.Response == Enums.ResponseStatuses.OK)
                    {
                        List<Point> furthestPointsFromCenter = CalculateFurthestPointsFromCenter(openFileResponse.Points);

                        if (furthestPointsFromCenter != null && furthestPointsFromCenter.Count > 0)
                        {
                            response.Response = Enums.ResponseStatuses.OK;
                            response.FurthestPointsFromCenter = furthestPointsFromCenter;

                            CheckTargetAndCreateFileIfNecessary(request.Target, furthestPointsFromCenter);
                        }
                        else
                        {
                            response.Response = Enums.ResponseStatuses.NoDataFound;
                            response.ResponseMessage = Enums.ResponseStatuses.NoDataFound.ToString();
                        }
                    }
                    else
                    {
                        response.Response = openFileResponse.Response;
                        response.ResponseMessage = openFileResponse.ResponseMessage;
                    }

                    // Log output
                    SerilogLogger.TraceLogOut(request.CorrelationID, response.Response);
                }
                catch (Exception ex)
                {
                    response.Response = Enums.ResponseStatuses.Exception;
                    response.ResponseMessage = ex.Message;

                    // Trace catch
                    SerilogLogger.TraceCatch(request.CorrelationID, ex);
                }
            }
            else
            {
                response.Response = Enums.ResponseStatuses.NullRequest;
                response.ResponseMessage = Enums.ResponseStatuses.NullRequest.ToString();
            }

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

            if (points != null && points.Count > 0)
            {
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
            }

            return results.Select(x => x.Point).ToList();
        }

        /// <summary>
        /// Checks the target and create file if necessary.
        /// </summary>
        /// <param name="target">The target.</param>
        /// <param name="furthestPointsFromCenter">The furthest points from center.</param>
        private void CheckTargetAndCreateFileIfNecessary(string target, List<Point> furthestPointsFromCenter)
        {
            if (target.ToLower() != Literals.Console)
            {
                using (TextWriter tw = new StreamWriter(target))
                {
                    foreach (Point p in furthestPointsFromCenter)
                    {
                        tw.WriteLine($"Point{p.No}({p.Coordinates.X},{p.Coordinates.Y}) in {p.GetQuadrant()}");
                    }
                }
            }
        }

        /// <summary>
        /// Reads the file.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        private OpenFileResponse ReadFile(OpenFileRequest request)
        {
            OpenFileResponse response = new OpenFileResponse();

            // Log input
            SerilogLogger.TraceLogIn(request.CorrelationID, request);

            if (File.Exists(request.FileName))
            {
                using (StreamReader sr = File.OpenText(request.FileName))
                {
                    string[] lineValues = sr.ReadToEnd().Split(Environment.NewLine);

                    foreach (string line in lineValues)
                    {
                        try
                        {
                            string purifiedLine = Regex.Replace(line, "[^0-9,()-]", string.Empty);

                            int no = int.Parse(purifiedLine.Substring(0, purifiedLine.IndexOf("(")));
                            int x = int.Parse(purifiedLine.Substring(purifiedLine.IndexOf("(") + 1, purifiedLine.IndexOf(",") - purifiedLine.IndexOf("(") - 1));
                            int y = int.Parse(purifiedLine.Substring(purifiedLine.IndexOf(",") + 1, purifiedLine.IndexOf(")") - purifiedLine.IndexOf(",") - 1));

                            response.Points.Add(new Point(no, x, y));
                        }
                        catch
                        {
                            SerilogLogger.TraceError(request.CorrelationID, string.Concat(Literals.CantParse, line));
                        }
                    }
                }

                if (response.Points != null && response.Points.Any())
                {
                    response.Response = Enums.ResponseStatuses.OK;
                }
                else
                {
                    response.Response = Enums.ResponseStatuses.NoDataFound;
                    response.ResponseMessage = Enums.ResponseStatuses.NoDataFound.ToString();
                }
            }
            else
            {
                response.Response = Enums.ResponseStatuses.FileDoesntExist;
                response.ResponseMessage = Enums.ResponseStatuses.FileDoesntExist.ToString();
            }

            // Log output
            SerilogLogger.TraceLogOut(request.CorrelationID, response.Points.Count);

            return response;
        }
    }
}