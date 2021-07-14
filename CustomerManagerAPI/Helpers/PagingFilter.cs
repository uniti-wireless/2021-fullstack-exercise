using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerAPI.Helpers
{
    // This class is generated to apply filter by for paging purpose
    public class PagingFilter
    {
        // max page size is set to 100
        const int maxPageSize = 100;

        // Current page number
        public int PageNumber { get; set; } = 1;

        // Page size
        private int pageSize = 10;

        // Get/ set page size
        public int PageSize
        {
            get
            {
                return pageSize;
            }
            set
            {
                if (value > maxPageSize)
                {
                    pageSize = maxPageSize;
                }
                else
                {
                    pageSize = value;
                }
            }
        }
    }
}
