using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BillingSystem.Shared.Helper.Security.Hashing
{

    public class HashingHelper
    {
        public static string CreatePasswordHash(string password)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                return Convert.ToBase64String(hashBytes);
            }
        }

        public static bool VerifyPasswordHash(string password, string storedHash)
        {
            using (var sha256 = System.Security.Cryptography.SHA256.Create())
            {
                var computedHash = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                var computedHashString = Convert.ToBase64String(computedHash);
                return computedHashString == storedHash;
            }
        }
    }

}
