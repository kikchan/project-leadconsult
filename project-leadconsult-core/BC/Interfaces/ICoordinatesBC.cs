using project_leadconsult_core.BE;

namespace project_leadconsult_core.BC
{
    /// <summary>
    /// ICoordinatesBC
    /// </summary>
    public interface ICoordinatesBC
    {
        /// <summary>
        /// Processes the file.
        /// </summary>
        /// <param name="request">The request.</param>
        /// <returns></returns>
        ProcessFileResponse ProcessFile(ProcessFileRequest request);
    }
}