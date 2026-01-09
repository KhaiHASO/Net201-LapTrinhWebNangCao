using Demo03.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Demo03.Controllers
{
    public class StudentsController : Controller
    {
        [HttpGet]
        public IActionResult Create()
        {
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>();
            ViewBag.AllBranches = Enum.GetValues(typeof(Branch)).Cast<Branch>();
            ViewBag.AllHobbies = new List<string> { "Music", "Sports", "Reading", "Travel", "Coding", "Gaming" };

            return View();
        }

        [HttpPost]
        public IActionResult Create(Student student)
        {
            if (ModelState.IsValid)
            {
                // Successful binding
                return Content($"Success! Student: {student.Name}, " +
                               $"Branch: {student.Branch}, " +
                               $"Address: {student.Address.Street}, {student.Address.City}, " +
                               $"Hobbies: {string.Join(", ", student.Hobbies)}");
            }

            // Reload data for View if validation fails
            ViewBag.AllGenders = Enum.GetValues(typeof(Gender)).Cast<Gender>();
            ViewBag.AllBranches = Enum.GetValues(typeof(Branch)).Cast<Branch>();
            ViewBag.AllHobbies = new List<string> { "Music", "Sports", "Reading", "Travel", "Coding", "Gaming" };

            return View(student);
        }
    }
}
