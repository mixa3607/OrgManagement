using System.IO;
using System.Security.Cryptography.X509Certificates;
using Microsoft.IdentityModel.Tokens;

namespace ManagementWebApi
{
    public class AuthOptions
    {
        public int RefreshTokenLifetimeDays { get; set; }

        public string JwtIssuer { get; set; }
        public string JwtAudience { get; set; }
        public int JwtLifetimeMins { get; set; }

        public string PublicCrtPath { get; set; }
        public string PrivatePfxPath { get; set; }

        public X509SecurityKey PrivateKey { get; private set; }
        public X509SecurityKey PublicKey { get; private set; }

        public void Init()
        {
            PublicKey = new X509SecurityKey(new X509Certificate2(File.ReadAllBytes(PublicCrtPath)));
            PrivateKey = new X509SecurityKey(new X509Certificate2(File.ReadAllBytes(PrivatePfxPath)));
        }
    }
}