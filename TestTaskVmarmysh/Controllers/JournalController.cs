using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;
using TestTaskVmarmysh.Common.Pagging;
using TestTaskVmarmysh.Services.Entities;
using TestTaskVmarmysh.Services.Entities.Filters;
using TestTaskVmarmysh.Services.Interfaces;

namespace TestTaskVmarmysh.Controllers
{
    /// <summary>
    /// Controller for journal methods.
    /// </summary>
    [ApiController]
    public class JournalController : ControllerBase
    {
        private readonly IJournalService _journalService;
        private readonly ILogger<JournalController> _logger;

        /// <summary>
        /// Constructor of journal controller.
        /// </summary>
        /// <param name="journalService">Service for access to journal.</param>
        /// <param name="logger">Journal controller loger.</param>
        public JournalController(IJournalService journalService, ILogger<JournalController> logger)
        {
            _journalService = journalService;
            _logger = logger;
        }

        /// <summary>
        /// Get journal item by id.
        /// </summary>
        /// <param name="id">Id of journal item.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Journal item view.</returns>
        [HttpPost("api.user.journal.getSingle")]
        public Task<JournalItemView> GetSingle([FromQuery, Required] int id, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(GetSingle)}. {nameof(id)}={id}.");

            return _journalService.GetSingle(id, token);
        }

        /// <summary>
        /// Get range of journal items.
        /// </summary>
        /// <param name="skip">Skip items count.</param>
        /// <param name="take">Take items count.</param>
        /// <param name="filter">Items filter.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Paged result of items.</returns>
        [HttpPost("api.user.journal.getRange")]
        public Task<PagedResult<JournalListItemView>> GetRange([FromQuery, Required] int skip, [FromQuery, Required] int take, [FromBody, Required] RangeFilter filter, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(GetRange)}. {nameof(skip)}={skip}, {nameof(take)}={take}, {nameof(filter)}={JsonSerializer.Serialize(filter)}.");

            return _journalService.GetRange(skip, take, filter, token);
        }
    }
}
