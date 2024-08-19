using TestTaskVmarmysh.Common.Pagging;
using TestTaskVmarmysh.Services.Entities;
using TestTaskVmarmysh.Services.Entities.Filters;

namespace TestTaskVmarmysh.Services.Interfaces
{
    /// <summary>
    /// Interface for access to journal.
    /// </summary>
    public interface IJournalService
    {
        /// <summary>
        /// Create journal item.
        /// </summary>
        /// <param name="eventId">Event id.</param>
        /// <param name="createdAt">Journal item created at.</param>
        /// <param name="text">Journal item text.</param>
        /// <returns>Create task.</returns>
        Task Create(long eventId, DateTime createdAt, string text);

        /// <summary>
        /// Get single journal item by id.
        /// </summary>
        /// <param name="id">Id of journal item.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Journal item view.</returns>
        Task<JournalItemView> GetSingle(int id, CancellationToken token);

        /// <summary>
        /// Get range of journal items via pagination and filter.
        /// </summary>
        /// <param name="skip">Skip quantity.</param>
        /// <param name="take">Get quantity.</param>
        /// <param name="filter">Items filter.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Range of journal items via pagination and filter.</returns>
        Task<PagedResult<JournalListItemView>> GetRange(int skip, int take, RangeFilter filter, CancellationToken token);
    }
}
