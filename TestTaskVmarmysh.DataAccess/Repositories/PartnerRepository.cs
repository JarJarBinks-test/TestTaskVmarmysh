using Microsoft.Extensions.Logging;
using TestTaskVmarmysh.Common.Exceptions;
using TestTaskVmarmysh.DataAccess.Context;
using TestTaskVmarmysh.DataAccess.Entities.JournalEntities;
using TestTaskVmarmysh.DataAccess.Interfaces;

namespace TestTaskVmarmysh.DataAccess.Repositories
{
    /// <summary>
    /// Repository for acces to partner.
    /// <seealso cref="TestTaskVmarmysh.DataAccess.Interfaces.IPartnerRepository"/> successor.
    /// </summary>
    public class PartnerRepository : IPartnerRepository
    {
        private readonly PartnerContext _context;
        private readonly ILogger<PartnerRepository> _logger;

        /// <summary>
        /// Constructor of <seealso cref="TestTaskVmarmysh.DataAccess.Repositories.PartnerRepository"/>
        /// </summary>
        /// <param name="context">Partner database context.</param>
        /// <param name="logger">Partner repository loger.</param>
        public PartnerRepository(PartnerContext context, ILogger<PartnerRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        /// <inheritdoc />
        public async Task RememberMe(string code, CancellationToken token)
        {
            _logger.LogInformation($"{nameof(RememberMe)}. {nameof(code)}={code}.");

            if (string.IsNullOrWhiteSpace(code))
            {
                throw new WrongParameterException(nameof(code));
            }

            var result = await _context.RememberMe.AddAsync(new RememberMe() { Code = code });
            await _context.SaveChangesAsync();
        }
    }
}
