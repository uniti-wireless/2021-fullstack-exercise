using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CustomerManagerAPI.Configuration
{
    // This class provides the list of page data to output for fromnt end purposes.
    public class PaginatedList<T> : List<T>
    {
        // Current page being browsed
        public int CurrentPage { get; private set; }

        // Total number of pages
        public int TotalPages { get; private set; }

        // Page size, i.e. number of items per page
        public int PageSize { get; private set; }

        // Total number of items
        public int TotalCount { get; private set; }

        // Previous page exists from this page
        public bool HasPrevious
        {
            get
            {
                if (CurrentPage > 1)
                {
                    return true;
                }
                return false;
            }
        }

        // Nest page exists from this page
        public bool HasNext
        {
            get
            {
                if (CurrentPage < TotalPages)
                {
                    return true;
                }
                return false;
            }
        }

        //Constructor
        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            TotalCount = count;
            PageSize = pageSize;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);

            AddRange(items);
        }

        // Convert source data into Paginated List for front end purposes
        public static PaginatedList<T> ToPaginatedList(List<T> source, int pageNumber, int pageSize)
        {
            int count = source.Count();
            List<T> items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }
}
