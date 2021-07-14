using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerAPI.Helpers
{
    public class SecretKeyGenerator
    {
        private Random random = new Random();

        private const string CHARS = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";

        // Generate a random secret key
        public string GenerateSecretKey()
        {
            return new string(Enumerable.Repeat(CHARS, 32)
                  .Select(s => s[random.Next(s.Length)]).ToArray());
        }
    }
}
