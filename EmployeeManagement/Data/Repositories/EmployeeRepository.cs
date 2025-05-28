using EmployeeManagement.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        private readonly AppDbContext _context;

        public EmployeeRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<List<Employee>> GetAllAsync() => await _context.Employees.ToListAsync();
        public async Task<Employee> GetByIdAsync(int id) => await _context.Employees.FindAsync(id);
        public async Task AddAsync(Employee employee) => await _context.Employees.AddAsync(employee);
        public async Task UpdateAsync(Employee employee) => _context.Employees.Update(employee);
        public async Task DeleteAsync(int id) => _context.Employees.Remove(await GetByIdAsync(id));
        public async Task<int> CountActiveAsync() => await _context.Employees.CountAsync(e => e.IsActive);
        public async Task<int> CountInactiveAsync() => await _context.Employees.CountAsync(e => !e.IsActive);
        public async Task<(IEnumerable<Employee> Employees, int TotalCount)> GetEmployeesAsync(
             string searchString = null,
             string sortOrder = "name",
             bool isActive = true,
             int pageNumber = 1,
             int pageSize = 15)
        {
            IQueryable<Employee> query = _context.Employees;

            if (!string.IsNullOrEmpty(searchString))
            {
                query = query.Where(e => e.Name.ToLower().Contains(searchString.ToLower()));
            }

            query = sortOrder switch
            {
                "name_desc" => query.OrderByDescending(e => e.Name),
                "date" => query.OrderBy(e => e.HireDate),
                "date_desc" => query.OrderByDescending(e => e.HireDate),
                _ => query.OrderBy(e => e.Name),
            };

            var totalCount = await query.CountAsync();
            var employees = await query
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            return (employees, totalCount);
        }
    }
}