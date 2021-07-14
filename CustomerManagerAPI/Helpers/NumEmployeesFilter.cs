using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerAPI.Helpers
{
    // This class is generated to apply filter by number of employees in customer objects
    public class NumEmployeesFilter
    {
        // Minimum number of employees for a customer
        public int MinNumEmployees { get; set; }
        // Maximum number of employees for a customer
        public int MaxNumEmployees { get; set; }
    }
}
