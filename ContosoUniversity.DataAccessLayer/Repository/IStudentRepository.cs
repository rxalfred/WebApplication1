using ContosoUniversity.DataAccessLayer.Repository;
using ContosoUniversity.DataAccessLayer.Repository.Base;
using ContosoUniversity.Model.CoreTestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ContosoUniversity.DataAccessLayer.Repository
{
    public interface IStudentRepository : IRepository<Student>
    {
        Task<IEnumerable<Student>> GetAllStudents();
        void Remove(long key);
        Task<int> RemoveExtended(long key);
        Task<int> UpdateExtended(Student item);
        Task<Student> GetStudentWithEnrollment(long key);
    }
}
