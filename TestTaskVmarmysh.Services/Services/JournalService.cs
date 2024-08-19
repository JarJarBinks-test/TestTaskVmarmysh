using Microsoft.Extensions.Logging;
using System.Text.Json;
using TestTaskVmarmysh.Common.Exceptions;
using TestTaskVmarmysh.Common.Pagging;
using TestTaskVmarmysh.DataAccess.Interfaces;
using TestTaskVmarmysh.Services.Entities;
using TestTaskVmarmysh.Services.Entities.Filters;
using TestTaskVmarmysh.Services.Interfaces;

namespace TestTaskVmarmysh.Services.Services
{
    /// <summary>
    /// Service for acces to journal.
    /// <seealso cref="TestTaskVmarmysh.Services.Interfaces.IJournalService"/> successor.
    /// </summary>
    public class JournalService : IJournalService
    {
        private readonly IJournalRepository _repository;
        private readonly ILogger<JournalService> _logger;

        public JournalService(IJournalRepository repository, ILogger<JournalService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <inheritdoc />
        public Task Create(long eventId, DateTime createdAt, string text)
        {
            _logger.LogInformation($"{nameof(Create)}. {nameof(eventId)}={eventId}, {nameof(createdAt)}={createdAt}, {nameof(text)}={text}.");

            if (eventId <= 0)
            {
                throw new WrongParameterException(nameof(eventId));
            }

            if (string.IsNullOrWhiteSpace(text))
            {
                throw new WrongParameterException(nameof(text));
            }

            return _repository.Create(eventId, createdAt, text);
        }

        /// <inheritdoc />
        public async Task<JournalItemView> GetSingle(int id, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(GetSingle)}. {nameof(id)}={id}.");

            if (id <= 0)
            {
                throw new WrongParameterException(nameof(id));
            }

            var result = await _repository.GetSingle(id, token);
            return new JournalItemView()
            {
                Id = result.Id,
                Text = result.Text,
                EventId = result.EventId.ToString(),
                CreatedAt = result.CreatedAt,
            };
        }

        /// <inheritdoc />
        public async Task<PagedResult<JournalListItemView>> GetRange(int skip, int take, RangeFilter filter, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(GetRange)}. {nameof(skip)}={skip}, {nameof(take)}={take}, {nameof(filter)}={JsonSerializer.Serialize(filter)}.");

            if (skip < 0)
            {
                throw new WrongParameterException(nameof(skip));
            }

            if (take <= 0)
            {
                throw new WrongParameterException(nameof(take));
            }

            if (filter == null)
            {
                throw new WrongParameterException(nameof(filter));
            }
            var result = await _repository.GetRange(skip, take, filter.From, filter.To, filter.Search, token);
            return new PagedResult<JournalListItemView>()
            {
                Count = result.Count,
                Skip = result.Skip,
                Items = result.Items.Select(resultItem =>
                    new JournalListItemView()
                    {
                        Id = resultItem.Id,
                        CreatedAt = resultItem.CreatedAt,
                        EventId = resultItem.EventId.ToString(),
                    }).ToList()
            };
        }
    }
}
