using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NET201Slide8Demo02.Data;

namespace NET201Slide8Demo02.Controllers
{
    public class StudentController : Controller
    {
        private readonly AppDbContext _context;

        public StudentController(AppDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            // Eager Loading Demo:
            // 1. .Include(s => s.Branch) -> Inner Join (Vì Branch là Bắt buộc)
            // 2. .Include(s => s.Address) -> Left Join (Vì Address là Nullable/Tùy chọn)
            
            var students = _context.Students
                                   .Include(s => s.Branch)
                                   .Include(s => s.Address)
                                   .ToList();

            return View(students);
        }
    }
}
