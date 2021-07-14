using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerAPI.Models
{
    // This clase store the values for customer objects
    public class Customer
    {
        // Unique custome ID
        public string Id { get; set; }

        // Number of employees for the customer
        public long NumberOfEmployees { get; set; }

        // Customer name
        public string Name { get; set; }

        // Tags for the customer business type
        public List<string> Tags { get; set; } = new List<string>();
    }
}
