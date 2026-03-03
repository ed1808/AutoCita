using System.Security.Cryptography;
using System.Text;

namespace AutoCita.Utils
{
    internal sealed class Auth
    {
        private const byte _keySize = 32;
        private const int _iterations = 350000;
        private readonly HashAlgorithmName _hashAlgorithm = HashAlgorithmName.SHA256;

        public string HashPassword(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_keySize);
            var hash = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                _iterations,
                _hashAlgorithm,
                _keySize
            );

            return $"{Convert.ToBase64String(hash)}.{Convert.ToBase64String(salt)}";
        }

        public bool VerifyPassword(string password, string storedPassword)
        {
            var parts = storedPassword.Split('.', 2);

            if (parts.Length != 2)
            {
                return false;
            }

            var hash = Convert.FromBase64String(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);

            var hashToCompare = Rfc2898DeriveBytes.Pbkdf2(
                Encoding.UTF8.GetBytes(password),
                salt,
                _iterations,
                _hashAlgorithm,
                _keySize
            );

            return CryptographicOperations.FixedTimeEquals(hash, hashToCompare);
        }
    }
}
