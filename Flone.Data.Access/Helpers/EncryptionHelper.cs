namespace Flone.Data.Repository.Helpers
{
    using BCrypt = BCrypt.Net.BCrypt;
    public static class EncryptionHelper
    {
        public static string WithBCrypt(this string text)=> BCrypt.HashPassword(text);
        public static bool VerifyWithBCrypt(this string hashedPassword, string plainText) => BCrypt.Verify(plainText, hashedPassword);
    }
}
