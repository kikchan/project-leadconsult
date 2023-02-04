using System;

namespace project_leadconsult_core.BE
{
    /// <summary>
    /// BaseRequest
    /// </summary>
    public class BaseRequest
    {
        /// <summary>
        /// The correlation identifier
        /// </summary>
        public readonly Guid CorrelationID;

        /// <summary>
        /// Initializes a new instance of the <see cref="BaseRequest"/> class.
        /// </summary>
        /// <param name="correlationID">The correlation identifier.</param>
        /// <exception cref="System.InvalidProgramException">CorrelationID can't be null or empty!</exception>
        public BaseRequest(Guid correlationID)
        {
            if (correlationID == Guid.Empty)
            {
                throw new InvalidProgramException("CorrelationID can't be empty!");
            }

            CorrelationID = correlationID;
        }
    }
}