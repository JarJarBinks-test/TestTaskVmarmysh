namespace TestTaskVmarmysh.DataAccess.Interfaces
{
    /// <summary>
    /// Interface for access to partner data.
    /// </summary>
    public interface IPartnerRepository
    {
        /// <summary>
        /// Remember me by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Remember me task.</returns>
        Task RememberMe(string code, CancellationToken token);
    }
}
