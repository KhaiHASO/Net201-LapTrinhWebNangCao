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
            // 1. .Include(s => s.Branch) -> Inner Join (Because Branch is Required)
            // 2. .Include(s => s.Address) -> Left Join (Because Address is Nullable/Optional)
            
            var students = _context.Students
                                   .Include(s => s.Branch)//inner join
                                   .Include(s => s.Address)//left join
                                   .ToList();

            return View(students);
        }
    }
}
