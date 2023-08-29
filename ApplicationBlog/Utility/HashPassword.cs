using System.Security.Cryptography;
using System.Text;

namespace ApplicationBlog.Utility
{
    public static class HashPassword
    {
        public static string GetHashPassword(string PlainPassword)
        {
            using (var sha256 = SHA256.Create())
            {
                // Send a sample text to hash.  
                var hashedBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(PlainPassword));
                // Get the hashed string.  
                return BitConverter.ToString(hashedBytes).Replace("-", "").ToLower();
            }
        }
    }
}
