using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Demo03.Models;
using Demo03.Data;
using Microsoft.EntityFrameworkCore;

namespace Demo03.Controllers;

public class HomeController : Controller
{
    private readonly Demo03Context _context;

    public HomeController(Demo03Context context)
    {
        _context = context;
    }

    public async Task<IActionResult> Index()
    {
        // Thống kê cho dashboard
        ViewBag.TotalAirports = await _context.Airports.CountAsync();
        ViewBag.TotalFlights = await _context.Flights.CountAsync();
        
        // Lấy danh sách chuyến bay gần đây
        var recentFlights = await _context.Flights
            .Include(f => f.DepartureAirport)
            .Include(f => f.ArrivalAirport)
            .OrderByDescending(f => f.DepartureTime)
            .Take(5)
            .ToListAsync();
        
        ViewBag.RecentFlights = recentFlights;
        
        // Lấy danh sách sân bay
        var airports = await _context.Airports
            .Include(a => a.DepartingFlights)
            .Include(a => a.ArrivingFlights)
            .ToListAsync();
        
        ViewBag.Airports = airports;

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
