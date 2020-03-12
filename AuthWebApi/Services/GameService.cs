using AuthWebApi.Database;
using Microsoft.Extensions.Logging;
using WebApiSharedParts.Enums;

namespace AuthWebApi.Services
{
    public class ChallengeNotifierService : IChallengeNotifierService
    {
        private readonly ILogger<ChallengeNotifierService> _logger;
        public ChallengeNotifierService(ILogger<ChallengeNotifierService> logger) => _logger = logger;
        public void Notify(string userName, string content)
        {
            _logger.LogDebug("Notify message \"{Message}\" to user {UserName}", content, userName);
            //TODO: need implementation
        }
    }
}