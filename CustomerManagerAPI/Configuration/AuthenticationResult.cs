using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerAPI.Configuration
{
    // Configration class to store login result data
    public class AuthenticationResult
    {
        // JWT token generated from user login.
        public string Token { get; set; }

        // True if valid user signed in successfully, false otherwise.
        public bool LoginSuccess { get; set; }

        // Custom error message to disply to the user.
        public string ErrorMessage { get; set; }
    }
}
