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
        private readonly Point center = new Point(0, 0, 0);

        /// <summary>
        /// Processes the file.
        /// </summary>
        /// <param name="filename">The filename.</param>
        public void ProcessFile(string filename)
        {
            List<Point> points = ReadFile(filename);
            List<Point> furthestPointsFromCenter = CalculateFurthestPointsFromCenter(points);
            
        }

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
            int takeNElements = 0;

            if (results.Count > 1)
            {
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
            }

            //return results.OrderByDescending(x => x.Distance).Select(x => x.Point).ToList();
            return results.Take(takeNElements).Select(x => x.Point).ToList().OrderBy(x => x.No).ToList();
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