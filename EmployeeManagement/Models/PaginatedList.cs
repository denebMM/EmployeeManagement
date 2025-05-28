using System;
using System.Collections.Generic;
using System.Linq;

namespace EmployeeManagement.Models  
{
    public class PaginatedList<T> : List<T>
    {
        public int PageIndex { get; private set; }
        public int TotalPages { get; private set; }
        public int TotalCount { get; private set; }
        public int PageSize { get; private set; }  
        public PaginatedList(List<T> items, int totalCount, int pageIndex, int pageSize)
        {
            PageIndex = pageIndex;
            TotalCount = totalCount;
            PageSize = pageSize; 
            TotalPages = (int)Math.Ceiling(totalCount / (double)pageSize);

            this.AddRange(items);
        }

        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        public static PaginatedList<T> Create(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageIndex, pageSize);
        }
    }
}