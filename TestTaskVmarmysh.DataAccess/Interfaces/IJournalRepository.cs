using TestTaskVmarmysh.Common.Pagging;
using TestTaskVmarmysh.DataAccess.Entities.JournalEntities;

namespace TestTaskVmarmysh.DataAccess.Interfaces
{
    /// <summary>
    /// Interface for access to journal data.
    /// </summary>
    public interface IJournalRepository
    {
        /// <summary>
        /// Create journal item.
        /// </summary>
        /// <param name="eventId">Event id.</param>
        /// <param name="createdAt">Event created at.</param>
        /// <param name="text">Event text.</param>
        /// <returns>Create task.</returns>
        Task Create(long eventId, DateTime createdAt, string text);

        /// <summary>
        /// Get single journal item by id.
        /// </summary>
        /// <param name="id">Id of journal item.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Journal item view.</returns>
        Task<Journal> GetSingle(int id, CancellationToken token);

        /// <summary>
        /// Get range of journal items via pagination and filter.
        /// </summary>
        /// <param name="skip">Skip quantity.</param>
        /// <param name="take">Get quantity.</param>
        /// <param name="from">From filter.</param>
        /// <param name="to">To filter.</param>
        /// <param name="search">Search string.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Range of journal items via pagination and filter.</returns>
        Task<PagedResult<Journal>> GetRange(int skip, int take, DateTime? from, DateTime? to, String search, CancellationToken token);
    }
}
