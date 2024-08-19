namespace TestTaskVmarmysh.Services.Interfaces
{
    /// <summary>
    /// Interface for store request id.
    /// </summary>
    public interface IRequestIdStorage
    {
        /// <summary>
        /// Get id for currect request.
        /// </summary>
        /// <returns>Request identifier.</returns>
        long GetNextId();
    }
}
