using Microsoft.AspNetCore.Mvc;
using Demo01.Models;
using System.Collections.Generic;

namespace Demo01.Controllers
{
    public class UsersController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.Countries = new List<string> { "Vietnam", "USA", "Canada", "Japan", "France" };
            ViewBag.Hobbies = new List<string> { "Reading", "Traveling", "Music", "Sports", "Coding" };
            return View();
        }

        [HttpPost]
        public IActionResult Create([FromForm] User user)
        {
            if (ModelState.IsValid)
            {
                // Successful binding and validation
                // For demo purposes, we'll just return the View with a success message or the bound data.
                // In a real app, we would save to DB here.
                return Content($"User {user.Name} registered successfully! Email: {user.Email}, Country: {user.Country}, Hobbies: {string.Join(", ", user.Hobbies)}");
            }

            // If invalid, reload the ViewBag data so the form can be re-rendered properly
            ViewBag.Countries = new List<string> { "Vietnam", "USA", "Canada", "Japan", "France" };
            ViewBag.Hobbies = new List<string> { "Reading", "Traveling", "Music", "Sports", "Coding" };
            
            return View(user);
        }
    }
}
