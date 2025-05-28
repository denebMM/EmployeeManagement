using EmployeeManagement.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace EmployeeManagement.Data.Repositories
{
    public interface IEmployeeRepository
    {
        Task<List<Employee>> GetAllAsync();
        Task<Employee> GetByIdAsync(int id);
        Task AddAsync(Employee employee);
        Task UpdateAsync(Employee employee);
        Task DeleteAsync(int id);
        Task<int> CountActiveAsync();
        Task<int> CountInactiveAsync();

        Task<(IEnumerable<Employee> Employees, int TotalCount)> GetEmployeesAsync(
         string searchString = null,
         string sortOrder = "name",
         bool isActive = true,
         int pageNumber = 1,
         int pageSize = 15);
    }
}