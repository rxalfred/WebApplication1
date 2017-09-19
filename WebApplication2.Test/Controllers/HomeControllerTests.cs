using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using WebApplication2.Controllers;
using Xunit;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Model.CoreTestModel;
using System.Linq;
using ContosoUniversity.BusinessLogic.Logic;
using ContosoUniversity.DataAccessLayer.Repository;

namespace WebApplication2.Test.Controllers
{
    public class HomeControllerTests
    {
        private SchoolContext _context;
        private const string connectionString = "Server=DESKTOP-4CO0ANB;Database=ContosoUniversity1;Trusted_Connection=True;MultipleActiveResultSets=true;";
        public HomeControllerTests()
        {
            InitContext();
        }

        [Fact]
        public void Create_Student()
        {
            var repo = new StudentRepository(_context);
            var logic = new StudentLogic(repo);
            var controller = new StudentsController(_context, logic);

            // Act
            var result = controller.Create();

            // Assert
            var viewResult = Assert.IsType<ViewResult>(result);
            //var model = Assert.IsAssignableFrom<IEnumerable<StormSessionViewModel>>(viewResult.ViewData.Model);
            //Assert.Equal(2, model.Count());
        }

        [Fact]
        public async Task Creating_Student()
        {
            var repo = new StudentRepository(_context);
            var logic = new StudentLogic(repo);
            var controller = new StudentsController(_context, logic);
            var stud = new Student { FirstMidName = "Carson test", LastName = "Alexander test", EnrollmentDate = DateTime.Parse("2005-09-01"), DepartmentId = 1 };


            var result = await controller.Create(stud);
            //https://github.com/ivaylokenov/MyTested.AspNetCore.Mvc
            //result.Should().BeOfType<RedirectToActionResult>().Which.ActionName.Should().Be("Index");
        }

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<SchoolContext>().UseSqlServer(connectionString);
            _context = new SchoolContext(builder.Options);
        }
    }
}