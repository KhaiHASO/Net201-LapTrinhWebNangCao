using Xunit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Demo01.Controllers;
using Demo01.Data;
using Demo01.Models;
using System;
using System.Linq;

namespace Demo01.Tests
{
    public class StudentControllerTests
    {
        private ApplicationDbContext GetDatabaseContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;
            var databaseContext = new ApplicationDbContext(options);
            databaseContext.Database.EnsureCreated();
            return databaseContext;
        }

        [Fact]
        public void Create_ReturnsViewResult_WhenModelStateIsInvalid()
        {
            // Arrange
            var context = GetDatabaseContext();
            var controller = new StudentController(context);
            controller.ModelState.AddModelError("FullName", "Required");
            var newStudent = new Student { Email = "test@test.com" };

            // Act
            var result = controller.Create(newStudent);

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            Assert.Equal(newStudent, viewResult.Model);
        }

        [Fact]
        public void Create_RedirectsToIndex_WhenModelStateIsValid()
        {
            // Arrange
            var context = GetDatabaseContext();
            var controller = new StudentController(context);
            var newStudent = new Student
            {
                FullName = "Nguyen Van A",
                Email = "abc@fpt.edu.vn",
                Age = 20,
                Gpa = 8.5
            };

            // Act
            var result = controller.Create(newStudent);

            // Assert
            var redirectToActionResult = Assert.IsType<RedirectToActionResult>(result);
            Assert.Equal("Index", redirectToActionResult.ActionName);
            Assert.Equal(1, context.Students.Count());
        }
    }
}
