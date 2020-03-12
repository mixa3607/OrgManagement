using AuthWebApi.Database;
using AuthWebApi.DataModels;

namespace AuthWebApi.Extensions
{
    public static class DbTypeConvertExtension
    {
        public static DbUser ToDbUser(this UserRegistrationModels reg)
        {
            return new DbUser()
            {
                UserName = reg.UserName
            };
        }
    }
}