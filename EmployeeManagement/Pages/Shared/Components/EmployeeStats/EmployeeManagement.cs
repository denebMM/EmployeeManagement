using EmployeeManagement.Data;
using EmployeeManagement.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace EmployeeManagement.Pages.Shared.Components.EmployeeStats
{
    public class EmployeeStatsViewComponent : ViewComponent
    {
        private readonly AppDbContext _context;

        public EmployeeStatsViewComponent(AppDbContext context)
        {
            _context = context;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            var stats = new EmployeeStatsViewModel
            {
                Active = await _context.Employees.CountAsync(e => e.IsActive),
                Inactive = await _context.Employees.CountAsync(e => !e.IsActive)
            };
            return View("Stats", stats);
        }
    }
}