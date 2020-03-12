using AuthWebApi.Database;
using Microsoft.AspNetCore.Identity;

namespace AuthWebApi.Extensions
{
    public static class DbUserExtension
    {
        private static readonly PasswordHasher<DbUser> Hasher = new PasswordHasher<DbUser>();
        public static PasswordVerificationResult VerifyPassword(this DbUser dbUser, string providedPassword)
        {
            return Hasher.VerifyHashedPassword(dbUser, dbUser.PassHash, providedPassword);
        }

        public static string HashPassword(this DbUser dbUser, string providedPassword)
        {
            return Hasher.HashPassword(dbUser, providedPassword);
        }

        public static string HashPasswordAndSet(this DbUser dbUser, string providedPassword)
        {
            dbUser.PassHash = Hasher.HashPassword(dbUser, providedPassword);
            return dbUser.PassHash;
        }
    }
}