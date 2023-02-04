using System;

namespace project_leadconsult_core.BE
{
    /// <summary>
    /// ProcessFileRequest
    /// </summary>
    /// <seealso cref="project_leadconsult_core.BE.BaseRequest" />
    public class ProcessFileRequest : BaseRequest
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="ProcessFileRequest" /> class.
        /// </summary>
        /// <param name="correlationID">The correlation identifier.</param>
        public ProcessFileRequest(Guid correlationID) : base(correlationID)
        {
        }

        /// <summary>
        /// Gets or sets the name of the file.
        /// </summary>
        /// <value>
        /// The name of the file.
        /// </value>
        public string FileName { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The target.
        /// </value>
        public string Target { get; set; }
    }
}