using BCrypt.Net;

namespace DevBridgeAPI.UseCases.Util
{
    public static class HashingUtil
    {
        public static string HashPasswordWithSalt(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }
        public static bool VerifyPassword(string password, string hash)
        {
            return BCrypt.Net.BCrypt.Verify(password, hash);
        }
    }
}