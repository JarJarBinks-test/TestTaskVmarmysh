namespace TestTaskVmarmysh.Results
{
    /// <summary>
    /// Present result of exception for response.
    /// </summary>
    public class ExceptionResult
    {
        /// <summary>
        /// Request id.
        /// </summary>
        public string Id { get; set; } = string.Empty; // TODO: prevent .net core rounding.

        /// <summary>
        /// Type of the exception.
        /// </summary>
        public string Type { get; set; } = string.Empty;

        /// <summary>
        /// Data of the exception.
        /// </summary>
        public string Data { get; set; } = string.Empty;
    }
}
