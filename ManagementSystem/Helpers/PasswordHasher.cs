using System.Security.Cryptography;
namespace ManagementSystem.Helpers
{
    public class PasswordHasher
    {
        private static RNGCryptoServiceProvider rng = new();
        private static readonly int SaltSize = 16;
        private static readonly int HashSize = 20;
        private static readonly int Iterations = 10000;

        public static byte[] HashPassword(string password)
        {
            byte[] salt;
            rng.GetBytes(salt = new byte[SaltSize]);
            var key = new Rfc2898DeriveBytes(password, salt, Iterations);
            var hash = key.GetBytes(HashSize);

            var hashBytes = new byte[HashSize + SaltSize];
            Array.Copy(salt, 0 , hashBytes, 0, SaltSize);
            Array.Copy(hash, 0, hashBytes, SaltSize, HashSize);

            return hashBytes;
        }

        public static bool VerifyPassword(string password, byte[] passFromDB)
        {
            var salt = new byte[SaltSize];
            Array.Copy(passFromDB, 0, salt, 0, SaltSize);
            var key = new Rfc2898DeriveBytes(password, salt, Iterations);
            byte[] hash = key.GetBytes(HashSize);
            for (int i = 0; i < HashSize; i++)
            {
                if( passFromDB[i + SaltSize] != hash[i])
                    return false;
            }
            return true;
        }
    }
}
