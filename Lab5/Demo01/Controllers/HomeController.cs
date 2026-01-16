using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Demo01.Models;
using Demo01.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo01.Controllers;

public class HomeController : Controller
{
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Thống kê cho dashboard
        ViewBag.TotalDepartments = await _context.Departments.CountAsync();
        ViewBag.TotalEmployees = await _context.Employees.CountAsync();
        ViewBag.AverageSalary = await _context.Employees.AverageAsync(e => (double?)e.Salary) ?? 0;
        ViewBag.MaxSalary = await _context.Employees.MaxAsync(e => (decimal?)e.Salary) ?? 0;
        
        // Lấy nhân viên mới nhất
        var recentEmployees = await _context.Employees
            .Include(e => e.Department)
            .OrderByDescending(e => e.EmployeeId)
            .Take(5)
            .ToListAsync();
        
        ViewBag.RecentEmployees = recentEmployees;
        
        // Lấy phòng ban với số lượng nhân viên
        var departments = await _context.Departments
            .Include(d => d.Employees)
            .ToListAsync();
        
        ViewBag.Departments = departments;

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
