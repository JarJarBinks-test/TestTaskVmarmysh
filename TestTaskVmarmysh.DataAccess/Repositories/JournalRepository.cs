using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TestTaskVmarmysh.Common.Exceptions;
using TestTaskVmarmysh.Common.Pagging;
using TestTaskVmarmysh.DataAccess.Context;
using TestTaskVmarmysh.DataAccess.Entities.JournalEntities;
using TestTaskVmarmysh.DataAccess.Interfaces;

namespace TestTaskVmarmysh.DataAccess.Repositories
{
    /// <summary>
    /// Repository for acces to journal.
    /// <seealso cref="TestTaskVmarmysh.DataAccess.Interfaces.IJournalRepository"/> successor.
    /// </summary>
    public class JournalRepository : IJournalRepository
    {
        private readonly JournalContext _context;
        private readonly ILogger<JournalRepository> _logger;

        /// <summary>
        /// Constructor of <seealso cref="TestTaskVmarmysh.DataAccess.Repositories.JournalRepository"/>
        /// </summary>
        /// <param name="context">Journal database context.</param>
        /// <param name="logger">Journal repository loger.</param>
        public JournalRepository(JournalContext context, ILogger<JournalRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task Create(long eventId, DateTime createdAt, string text)
        {
            _logger.LogInformation($"{nameof(Create)}. {nameof(eventId)}={eventId}.");

            if (eventId <= 0)
            {
                throw new WrongParameterException(nameof(eventId));
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new WrongParameterException(nameof(text));
            }

            var result = await _context.Journal.AddAsync(new Journal() { EventId = eventId, CreatedAt = createdAt, Text = text });
            await _context.SaveChangesAsync();
        }

        /// <inheritdoc />
        public async Task<Journal> GetSingle(int id, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(GetSingle)}. {nameof(id)}={id}.");

            if (id <= 0)
            {
                throw new WrongParameterException(nameof(id));
            }

            var result = await _context.Journal.FindAsync(new object[] { id }, token);
            if (result == null)
            {
                throw new Exception($"The journal# {id} does not exist.");
            }

            return result;
        }

        /// <inheritdoc />
        public async Task<PagedResult<Journal>> GetRange(int skip, int take, DateTime? from, DateTime? to, String search, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(GetRange)}. {nameof(skip)}={skip}, {nameof(take)}={take}, {nameof(from)}={from}, {nameof(to)}={to}, {nameof(search)}={search}.");

            if (skip < 0)
            {
                throw new WrongParameterException(nameof(skip));
            }

            if (take <= 0)
            {
                throw new WrongParameterException(nameof(take));
            }

            var journalQuery = _context.Journal.Where(journal =>
                (!from.HasValue || journal.CreatedAt > from) &&
                (!to.HasValue || journal.CreatedAt <= to) &&
                (string.IsNullOrEmpty(search) || journal.Text.ToLower().Contains(search.Trim().ToLower()))); // TODO: Bad performance.

            var count = await journalQuery.CountAsync(token);
            var dataQuery = journalQuery.OrderByDescending(journal => journal.CreatedAt).AsQueryable();
            if (skip > 0)
            {
                dataQuery = dataQuery.Skip(skip);
            }
            dataQuery = dataQuery.Take(take);

            var resultQuery = from journal in dataQuery
                              select new Journal
                              {
                                  Id = journal.Id,
                                  EventId = journal.EventId,
                                  CreatedAt = journal.CreatedAt,
                              };
            var items = await resultQuery.ToListAsync(token);
            return new PagedResult<Journal>() { Count = count, Skip = skip, Items = items };
        }
    }
}
