namespace project_leadconsult_core.Enums
{
    /// <summary>
    /// ResponseStatuses
    /// </summary>
    public enum ResponseStatuses
    {
        /// <summary>
        /// The ok
        /// </summary>
        OK = 1,

        /// <summary>
        /// The unknown error
        /// </summary>
        UnknownError = -1,

        /// <summary>
        /// The null request
        /// </summary>
        NullRequest = -2,

        /// <summary>
        /// The exception
        /// </summary>
        Exception = -3,

        /// <summary>
        /// The no data found
        /// </summary>
        NoDataFound = -4
    }
}