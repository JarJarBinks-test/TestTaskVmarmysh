using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TestTaskVmarmysh.Services.Interfaces;

namespace TestTaskVmarmysh.Controllers
{
    /// <summary>
    /// Controller for partner methods.
    /// </summary>
    [ApiController]
    public class PartnerController : ControllerBase
    {
        private readonly IPartnerService _partnerService;
        private readonly ILogger<PartnerController> _logger;

        /// <summary>
        /// Constructor of partner controller.
        /// </summary>
        /// <param name="partnerService">Service for access to partner.</param>
        /// <param name="logger">Partner controller loger.</param>
        public PartnerController(IPartnerService partnerService, ILogger<PartnerController> logger)
        {
            _partnerService = partnerService;
            _logger = logger;
        }

        /// <summary>
        /// Remember me by code.
        /// </summary>
        /// <param name="code">The code.</param>
        /// <param name="token">The cancellation token.</param>
        /// <returns>Remember me task.</returns>
        [HttpPost("api.user.partner.rememberMe")]
        public Task RememberMe([FromQuery, Required] string code, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(RememberMe)}. {nameof(code)}={code}.");

            return _partnerService.RememberMe(code, token);
        }
    }
}
