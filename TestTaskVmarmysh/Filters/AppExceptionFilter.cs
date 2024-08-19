using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using TestTaskVmarmysh.Common.Exceptions;
using TestTaskVmarmysh.Results;
using TestTaskVmarmysh.Services.Interfaces;

namespace TestTaskVmarmysh.Filters
{
    /// <summary>
    /// Application exception filter.
    /// </summary>
    public class AppExceptionFilter : IExceptionFilter
    {
        private readonly IRequestIdService _requestIdService;
        private readonly IJournalService _journalService;
        private readonly ILogger<AppExceptionFilter> _logger;

        /// <summary>
        /// Application exception filter constructor.
        /// </summary>
        /// <param name="requestIdService">Service for get request id..</param>
        /// <param name="journalService">Journal service.</param>
        /// <param name="logger">Class loger.</param>
        public AppExceptionFilter(IRequestIdService requestIdService, IJournalService journalService, ILogger<AppExceptionFilter> logger)
        {
            _requestIdService = requestIdService;
            _journalService = journalService;
            _logger = logger;
        }

        /// <summary>
        /// Handle exception method.
        /// </summary>
        /// <param name="context">Exception context.</param>
        public void OnException(ExceptionContext context)
        {
            _logger.LogInformation($"{nameof(OnException)}. {nameof(_requestIdService.Id)}={_requestIdService.Id}, {nameof(context.Exception.Message)}={context.Exception.Message}.");
            LogException($"Request ID = {_requestIdService.Id}{Environment.NewLine} Path = {context.HttpContext.Request.Path}{Environment.NewLine}, {context.Exception.Message} {context.Exception.StackTrace}");

            var result = new ExceptionResult()
            {
                Id = _requestIdService.Id.ToString(),
                Type = context.Exception is BaseTypedException exception ? exception.Type : "Exception",
                Data = context.Exception.Message,
            };
            context.Result = new JsonResult(result)
            {
                StatusCode = (int)HttpStatusCode.InternalServerError,
            };
        }

        /// <summary>
        /// Write message in journal.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <returns>Wrating task.</returns>
        Task LogException(string message) {
            return _journalService.Create(_requestIdService.Id, DateTime.UtcNow, message);
        }
    }
}
