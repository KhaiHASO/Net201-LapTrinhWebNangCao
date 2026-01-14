using Demo02.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Demo02.Controllers
{
    public class DepartmentController : Controller
    {
        private readonly RelationshipContext _context;

        public DepartmentController(RelationshipContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            // Include Employees để hiển thị danh sách nhân viên trong mỗi phòng ban
            var departments = await _context.Departments
                                            .Include(d => d.Employees)
                                            .ToListAsync();
            return View(departments);
        }
    }
}
