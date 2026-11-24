using System.Security.Cryptography;
using System.Text;

namespace binks_forum_API.Helpers;

public static class Encryption
{
    private const int SaltSize = 32;
    private const int HashSize = 64;
    private const int Pbkdf2Iterations = 210_000;

    public static string HashPassword(string password)
    {
        ArgumentNullException.ThrowIfNull(password);
        var salt = RandomNumberGenerator.GetBytes(SaltSize);
        var hash = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(password), salt,
            Pbkdf2Iterations, HashAlgorithmName.SHA512, HashSize);
        return $"pbkdf2-sha512${Pbkdf2Iterations}${Convert.ToBase64String(salt)}${Convert.ToBase64String(hash)}";
    }

    public static bool VerifyPassword(string inputPassword, string storedPassword)
    {
        if (string.IsNullOrEmpty(inputPassword) || string.IsNullOrEmpty(storedPassword)) return false;
        if (storedPassword.StartsWith("pbkdf2-sha512$", StringComparison.Ordinal))
        {
            try
            {
                var parts = storedPassword.Split('$');
                if (parts.Length != 4 || !int.TryParse(parts[1], out var iterations) || iterations < 100_000 || iterations > 2_000_000)
                    return false;
                var salt = Convert.FromBase64String(parts[2]);
                var expected = Convert.FromBase64String(parts[3]);
                if (salt.Length != SaltSize || expected.Length != HashSize) return false;
                var actual = Rfc2898DeriveBytes.Pbkdf2(Encoding.UTF8.GetBytes(inputPassword), salt,
                    iterations, HashAlgorithmName.SHA512, expected.Length);
                return CryptographicOperations.FixedTimeEquals(actual, expected);
            }
            catch (FormatException) { return false; }
            catch (ArgumentException) { return false; }
        }

        // Preserve login compatibility with SHA-512 salted hashes created by earlier versions.
        try
        {
            var legacyParts = storedPassword.Split(':');
            if (legacyParts.Length != 2 || legacyParts[0].Length != SaltSize * 2 || legacyParts[1].Length != HashSize * 2)
                return false;
            var salt = Convert.FromHexString(legacyParts[0]);
            var expected = Convert.FromHexString(legacyParts[1]);
            var passwordBytes = Encoding.UTF8.GetBytes(inputPassword);
            var saltedPassword = new byte[salt.Length + passwordBytes.Length];
            Buffer.BlockCopy(salt, 0, saltedPassword, 0, salt.Length);
            Buffer.BlockCopy(passwordBytes, 0, saltedPassword, salt.Length, passwordBytes.Length);
            var actual = SHA512.HashData(saltedPassword);
            return CryptographicOperations.FixedTimeEquals(actual, expected);
        }
        catch (FormatException) { return false; }
    }
}
