using System.Security.Cryptography;
using System.Text;

namespace Model
{
    // Turns a plain-text password into a SHA-256 hash before it goes to the database.
    //
    // The database never stores the real password, only this 64-character hash.
    // At login the typed password is hashed the same way and the two hashes are
    // compared, so the original password is never needed again.
    //
    // Note: this is a simple approach suited to a learning project. A production
    // system would use a slow, salted algorithm such as bcrypt or PBKDF2.
    public static class PasswordHasher
    {
        public static string Hash(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));

                // Each byte becomes two hex characters, so the result is always 64 long.
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
