using AuthWebApi.Database;
using WebApiSharedParts.Enums;

namespace AuthWebApi.Services
{
    public interface ITokenGeneratorService
    {
        string GenerateToken(string userName, EUserRole userRole);
    }
}