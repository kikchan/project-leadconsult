using System.Collections.Generic;

namespace project_leadconsult_core.BE
{
    /// <summary>
    /// ProcessFileResponse
    /// </summary>
    /// <seealso cref="project_leadconsult_core.BE.BaseResponse" />
    public class ProcessFileResponse : BaseResponse
    {
        /// <summary>
        /// Gets the furthest points from center.
        /// </summary>
        /// <value>
        /// The furthest points from center.
        /// </value>
        public List<Point> FurthestPointsFromCenter { get; internal set; }
    }
}