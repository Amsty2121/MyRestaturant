using System.Security.Cryptography;
using System.Text;

namespace Common
{
    public static class PasswordHasher
    {
        public static byte[] Hash(string plainTextPassword)
        {
            var sha1 = new SHA1CryptoServiceProvider();
            var data = Encoding.ASCII.GetBytes(plainTextPassword);
            var sha1data = sha1.ComputeHash(data);

            return sha1data;
        }
    }
}