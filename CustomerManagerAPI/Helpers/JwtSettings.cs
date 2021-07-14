using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerAPI.Helpers
{
    // This class is used to store the secret key to use in the project as global value
    public class JwtSettings
    {
        // Secret key
        public string Secret { get; set; }
    }
}
