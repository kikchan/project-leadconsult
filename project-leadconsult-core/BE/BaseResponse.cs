using project_leadconsult_core.Enums;

namespace project_leadconsult_core.BE
{
    /// <summary>
    /// BaseResponse
    /// </summary>
    public class BaseResponse
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BaseResponse" /> class.
        /// </summary>
        public BaseResponse()
        {
            Response = ResponseStatuses.UnknownError;
        }

        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>
        /// The response.
        /// </value>
        public ResponseStatuses Response { get; set; }

        /// <summary>
        /// Gets or sets the response message.
        /// </summary>
        /// <value>
        /// The error message.
        /// </value>
        public string ResponseMessage { get; set; }
    }
}
