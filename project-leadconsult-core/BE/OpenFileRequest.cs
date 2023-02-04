using System;

namespace project_leadconsult_core.BE
{
    /// <summary>
    /// OpenFileRequest
    /// </summary>
    /// <seealso cref="project_leadconsult_core.BE.BaseRequest" />
    public class OpenFileRequest : BaseRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="OpenFileRequest"/> class.
        /// </summary>
        /// <param name="correlationID">The correlation identifier.</param>
        public OpenFileRequest(Guid correlationID) : base(correlationID)
        {
        }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }
    }
}