using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Demo02.Models;
using Demo02.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo02.Controllers;

public class HomeController : Controller
{
    private readonly DemoContext _context;

    public HomeController(DemoContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Lấy thống kê cho dashboard
        ViewBag.TotalClassRooms = await _context.ClassRooms.CountAsync();
        ViewBag.TotalStudents = await _context.Students.CountAsync();
        ViewBag.AverageGPA = await _context.Students.AverageAsync(s => (double?)s.GPA) ?? 0;
        ViewBag.HighestGPA = await _context.Students.MaxAsync(s => (decimal?)s.GPA) ?? 0;
        
        // Lấy top 5 sinh viên có điểm cao nhất
        var topStudents = await _context.Students
            .Include(s => s.ClassRoom)
            .OrderByDescending(s => s.GPA)
            .Take(5)
            .ToListAsync();
        
        ViewBag.TopStudents = topStudents;
        
        // Lấy danh sách lớp học với số lượng sinh viên
        var classRooms = await _context.ClassRooms
            .Include(c => c.Students)
            .ToListAsync();
        
        ViewBag.ClassRooms = classRooms;

        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
