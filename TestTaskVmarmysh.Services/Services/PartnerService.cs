using Microsoft.Extensions.Logging;
using TestTaskVmarmysh.Common.Exceptions;
using TestTaskVmarmysh.DataAccess.Interfaces;
using TestTaskVmarmysh.Services.Interfaces;

namespace TestTaskVmarmysh.Services.Services
{
    /// <summary>
    /// Service for acces to partner.
    /// <seealso cref="TestTaskVmarmysh.Services.Interfaces.IPartnerService"/> successor.
    /// </summary>
    public class PartnerService : IPartnerService
    {
        private readonly IPartnerRepository _repository;
        private readonly ILogger<PartnerService> _logger;

        /// <summary>
        /// Constructore of partner service.
        /// </summary>
        /// <param name="repository">Repository for access to partner.</param>
        /// <param name="logger">Partner service logger.</param>
        public PartnerService(IPartnerRepository repository, ILogger<PartnerService> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        /// <inheritdoc />
        public Task RememberMe(string code, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(RememberMe)}. {nameof(code)}={code}.");

            if (String.IsNullOrWhiteSpace(code))
            {
                throw new WrongParameterException(nameof(code));
            }

            return _repository.RememberMe(code, token);
        }
    }
}
