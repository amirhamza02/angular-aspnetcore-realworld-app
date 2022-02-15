using Microsoft.IdentityModel.Tokens;

namespace Flone.Security.Auth
{
    public class TokenAuthOption
    {
        public static string Audience { get; } = "FloneUser";
        public static string Issuer { get; } = "FloneIssuer";
        public static RsaSecurityKey Key { get; } = new RsaSecurityKey(RSAKeyHelper.GenerateKey());
        public static SigningCredentials SigningCredentials { get; } = new SigningCredentials(Key, SecurityAlgorithms.RsaSha256Signature);

        public static TimeSpan ExpiresSpan { get; } = TimeSpan.FromMinutes(60);
        public static string TokenType { get; } = "Bearer";
    }
}
