using EmployeeManagement.Data.Repositories;
using EmployeeManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Pages.Employees
{
    public class IndexModel : PageModel
    {
        private readonly IEmployeeRepository _employeeRepository;
        private const int PageSize = 15;

        public IndexModel(IEmployeeRepository employeeRepository)
        {
            _employeeRepository = employeeRepository;
        }

        public PaginatedList<Employee> Employees { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SearchString { get; set; }

        [BindProperty(SupportsGet = true)]
        public string SortOrder { get; set; }

        [BindProperty(SupportsGet = true)]
        public string CurrentFilter { get; set; }

        [BindProperty(SupportsGet = true)]
        public int CurrentPage { get; set; } = 1;

        public async Task OnGetAsync(string sortOrder, int? pageIndex)
        {
            SortOrder = sortOrder;
            CurrentFilter = SearchString;
            CurrentPage = pageIndex ?? 1;

            var (employees, totalCount) = await _employeeRepository.GetEmployeesAsync(
                searchString: SearchString,
                sortOrder: SortOrder,
                pageNumber: CurrentPage,
                pageSize: PageSize
            );

            Employees = new PaginatedList<Employee>(employees.ToList(), totalCount, CurrentPage, PageSize);
        }
    }
}