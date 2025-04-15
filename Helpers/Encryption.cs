using System.Security.Cryptography;
using System.Text;

namespace binks_forum_API.Helpers
{
    public static class Encryption
    {
        // Taille du sel en octets (par exemple 32)
        private const int SaltSize = 32;

        public static string HashPassword(string password)
        {
            // Générer un sel aléatoire
            byte[] salt = new byte[SaltSize];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            // Convertir le mot de passe en bytes et concaténer (sel + mot de passe)
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltedPassword = new byte[salt.Length + passwordBytes.Length];
            Buffer.BlockCopy(salt, 0, saltedPassword, 0, salt.Length);
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, salt.Length, passwordBytes.Length);

            // Calculer le hash avec SHA512
            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] hashBytes = sha512Hash.ComputeHash(saltedPassword);

                // Convertir le sel et le hash en chaînes hexadécimales
                StringBuilder saltBuilder = new StringBuilder();
                foreach (byte b in salt)
                {
                    saltBuilder.Append(b.ToString("x2"));
                }

                StringBuilder hashBuilder = new StringBuilder();
                foreach (byte b in hashBytes)
                {
                    hashBuilder.Append(b.ToString("x2"));
                }

                // Retourner le sel et le hash séparés par un deux-points
                return $"{saltBuilder}:{hashBuilder}";
            }
        }

        // Méthode de vérification du mot de passe
        public static bool VerifyPassword(string inputPassword, string storedPassword)
        {
            // Le format attendu est "sel:hash"
            var parts = storedPassword.Split(':');
            if (parts.Length != 2)
            {
                return false;
            }

            string saltHex = parts[0];
            string storedHash = parts[1];

            // Convertir le sel de hexadécimal en tableau d'octets
            byte[] salt = new byte[saltHex.Length / 2];
            for (int i = 0; i < salt.Length; i++)
            {
                salt[i] = Convert.ToByte(saltHex.Substring(i * 2, 2), 16);
            }

            // Concaténer le sel et le mot de passe d'entrée
            byte[] passwordBytes = Encoding.UTF8.GetBytes(inputPassword);
            byte[] saltedPassword = new byte[salt.Length + passwordBytes.Length];
            Buffer.BlockCopy(salt, 0, saltedPassword, 0, salt.Length);
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, salt.Length, passwordBytes.Length);

            // Calculer le hash du mot de passe fourni
            using (SHA512 sha512Hash = SHA512.Create())
            {
                byte[] computedHashBytes = sha512Hash.ComputeHash(saltedPassword);
                StringBuilder computedHashBuilder = new StringBuilder();
                foreach (byte b in computedHashBytes)
                {
                    computedHashBuilder.Append(b.ToString("x2"));
                }
                string computedHash = computedHashBuilder.ToString();

                // Comparer les hash en ignorant la casse
                return computedHash.Equals(storedHash, StringComparison.OrdinalIgnoreCase);
            }
        }
    }
}