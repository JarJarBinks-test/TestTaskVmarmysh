namespace TestTaskVmarmysh.Services.Interfaces
{
    /// <summary>
    /// Interface for access to partner.
    /// </summary>
    public interface IPartnerService
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
