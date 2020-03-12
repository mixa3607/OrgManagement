using AuthWebApi.Database;
using WebApiSharedParts.Enums;

namespace AuthWebApi.Services
{
    public interface IChallengeNotifierService
    {
        void Notify(string userName, string content);
    }
}