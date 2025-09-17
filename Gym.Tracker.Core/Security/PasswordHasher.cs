using System.Security.Cryptography;
using System.Text;
using Konscious.Security.Cryptography;

namespace Gym.Tracker.Core.Security
{
    public static class PasswordHasher
    {
        private const int SaltSize = 16;
        private const int HashSize = 32;
        private const int Iterations = 3;
        private const int DegreeOfParallelism = 2;
        private const int MemorySizeKb = 65536; // 64 MB

        public static byte[] HashPassword(string password)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));

            byte[] salt = new byte[SaltSize];
            RandomNumberGenerator.Fill(salt);

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = DegreeOfParallelism,
                MemorySize = MemorySizeKb,
                Iterations = Iterations
            };

            byte[] hash = argon2.GetBytes(HashSize);

            // Format: base64(salt)$base64(hash)$params
            string formatted = $"{Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}${Iterations}:{DegreeOfParallelism}:{MemorySizeKb}";

            return Encoding.UTF8.GetBytes(formatted);
        }

        public static bool VerifyPassword(string password, byte[] stored)
        {
            if (password == null) throw new ArgumentNullException(nameof(password));
            if (stored == null) return false;

            string storedFormatted = Encoding.UTF8.GetString(stored);
            var parts = storedFormatted.Split('$');
            if (parts.Length != 3) return false;

            byte[] salt = Convert.FromBase64String(parts[0]);
            byte[] hash = Convert.FromBase64String(parts[1]);
            var paramParts = parts[2].Split(':');
            if (paramParts.Length != 3) return false;
            if (!int.TryParse(paramParts[0], out int iterations)) return false;
            if (!int.TryParse(paramParts[1], out int parallelism)) return false;
            if (!int.TryParse(paramParts[2], out int memoryKb)) return false;

            var argon2 = new Argon2id(Encoding.UTF8.GetBytes(password))
            {
                Salt = salt,
                DegreeOfParallelism = parallelism,
                MemorySize = memoryKb,
                Iterations = iterations
            };

            byte[] computed = argon2.GetBytes(hash.Length);
            return CryptographicOperations.FixedTimeEquals(computed, hash);
        }
    }
}
