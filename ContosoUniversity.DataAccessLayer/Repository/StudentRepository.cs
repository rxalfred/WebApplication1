using ContosoUniversity.DataAccessLayer.Repository;
using ContosoUniversity.DataAccessLayer.Repository.Base;
using ContosoUniversity.Model.CoreTestModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Z.EntityFramework.Plus;

namespace ContosoUniversity.DataAccessLayer.Repository
{
    public class StudentRepository : Repository<Student>, IStudentRepository
    {
        private readonly SchoolContext _context;
        private readonly DbSet<Student> _students;

        public StudentRepository(SchoolContext context) : base (context)
        {
            //_context = context as SchoolContext;
            _context = context;
            _students = _context.Students;
        }

        public async Task<IEnumerable<Student>> GetAllStudents()
        {
            return await _context.Students.ToListAsync();
        }

        public async void Remove(long key)
        {
            var entity = _students.First(t => t.ID == key);
            _students.Remove(entity);
            await _context.SaveChangesAsync();
        }

        public async Task<int> RemoveExtended(long key)
        {            
            return await _students.Where(s => s.ID == key).DeleteAsync();
            //_context.Students.Where(x => x.LastLoginDate < DateTime.Now.AddYears(-2))
            //.Delete(x => x.BatchSize = 1000);
        }

        public async Task<int> UpdateExtended(Student student)
        {
            return await _students.Where(s=> s.ID == student.ID).UpdateAsync(s => new Student() { FirstMidName = student.FirstMidName });
        }

        // Explicitly Loading
        public async Task<Student> GetStudentWithEnrollment(long id)
        {
            var student = await _students.FirstAsync(t => t.ID == id);
            await _context.Entry(student)
                    .Collection(b => b.Enrollments)
                    .Query()
                    .Where(p => p.CourseID == 2)
                    .LoadAsync();

            return student;
        }
    }
}