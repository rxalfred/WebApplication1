using ContosoUniversity.BusinessLogic.Logic;
using ContosoUniversity.DataAccessLayer.Repository;
using ContosoUniversity.Model.CoreTestModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace WebApplication2.Test
{
    public class UnitTest1
    {
        private SchoolContext _context;
        private const string connectionString = "Server=DESKTOP-4CO0ANB;Database=ContosoUniversity1;Trusted_Connection=True;MultipleActiveResultSets=true;";
        public UnitTest1()
        {
            InitContext();
        }

        public void InitContext()
        {
            var builder = new DbContextOptionsBuilder<SchoolContext>().UseSqlServer(connectionString);
            _context = new SchoolContext(builder.Options);
        }

        [Fact]
        public async Task TestStudentWithEnrollment()
        {
            var repo = new StudentRepository(_context);
            var logic = new StudentLogic(repo);

            var student = await logic.TestStudentWithEnrollment();

            Assert.NotNull(student.Enrollments);
        }
    }
}
