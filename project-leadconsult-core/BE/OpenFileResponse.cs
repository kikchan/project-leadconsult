using System.Collections.Generic;

namespace project_leadconsult_core.BE
{
    /// <summary>
    /// OpenFileResponse
    /// </summary>
    /// <seealso cref="project_leadconsult_core.BE.BaseResponse" />
    public class OpenFileResponse : BaseResponse
    {
        /// <summary>
        /// Gets or sets the points.
        /// </summary>
        /// <value>
        /// The points.
        /// </value>
        public List<Point> Points { get; set; } = new List<Point>();
    }
}