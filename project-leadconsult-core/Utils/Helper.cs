using project_leadconsult_core.BE;
using System;
using System.Collections.Generic;
using System.Text;

namespace project_leadconsult_core.Utils
{
    public class Helper
    {
        /// <summary>
        /// Euclideans the plane.
        /// </summary>
        /// <see cref="https://en.wikipedia.org/wiki/Euclidean_distance"/>
        /// <param name="p1">The p1.</param>
        /// <param name="p2">The p2.</param>
        /// <returns></returns>
        public static double EuclideanPlane(Point p1, Point p2)
        {
            return Math.Sqrt(Math.Pow(p1.Coordinates.X - p2.Coordinates.X, 2) + Math.Pow(p1.Coordinates.Y - p2.Coordinates.Y, 2));
        }
    }
}
