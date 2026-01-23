using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo01.Data;
using Demo01.Models;

namespace Demo01.Controllers
{
    public class NativeSqlController : Controller
    {
        private readonly SchoolContext _context;

        public NativeSqlController(SchoolContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }

        // 1. FromSqlRaw (SELECT)
        public IActionResult RawQueryBasic()
        {
            // Case: Get students older than 18
            var ageLimit = 18;
            var students = _context.Students
                .FromSqlRaw("SELECT * FROM Students WHERE Age > {0}", ageLimit)
                .ToList();

            return View(students);
        }

        // 2. FromSqlInterpolated (SELECT)
        public IActionResult InterpolatedQuery(string searchName)
        {
            if (string.IsNullOrEmpty(searchName))
            {
                return View(new List<Student>());
            }

            // Case: Search by name (approximate)
            var term = $"%{searchName}%";
            var students = _context.Students
                .FromSqlInterpolated($"SELECT * FROM Students WHERE FullName LIKE {term}")
                .ToList();

            return View(students);
        }

        // 3. ExecuteSqlRaw (UPDATE)
        public IActionResult RawUpdate()
        {
            // Update 1 student for demo. Let's update student with Id = 1
            var affectedRows = _context.Database
                .ExecuteSqlRaw("UPDATE Students SET Age = Age + 1 WHERE Id = 1");

            ViewBag.Message = $"Updated {affectedRows} row(s). Student Id 1 age increased.";
            return View();
        }

        // 4. ExecuteSqlInterpolated (DELETE)
        [HttpPost]
        public IActionResult InterpolatedDelete(int id)
        {
            var affectedRows = _context.Database
                .ExecuteSqlInterpolated($"DELETE FROM Students WHERE Id = {id}");

            TempData["Message"] = $"Deleted {affectedRows} row(s) with Id {id}.";
            return RedirectToAction(nameof(Index));
        }

        [HttpGet]
        public IActionResult InterpolatedDelete()
        {
            return View();
        }

        // 5. SQL Injection Test (Unsafe)
        public IActionResult UnsafeQuery(string searchName)
        {
             if (string.IsNullOrEmpty(searchName))
            {
                return View(new List<Student>());
            }

            // BAD PRACTICE: String concatenation leading to SQL Injection
            var query = "SELECT * FROM Students WHERE FullName = '" + searchName + "'";
            var students = _context.Students
                .FromSqlRaw(query)
                .ToList();

            ViewBag.Query = query; // Display the query to show what happened
            return View(students);
        }
    }
}
