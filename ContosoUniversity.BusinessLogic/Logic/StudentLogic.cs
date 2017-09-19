using ContosoUniversity.DataAccessLayer.Repository;
using ContosoUniversity.Model.CoreTestModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContosoUniversity.BusinessLogic.Logic
{
    public class StudentLogic : IStudentLogic
    {
        private readonly IStudentRepository _studentRepository;

        public StudentLogic(IStudentRepository studentRepository)
        {
            _studentRepository = studentRepository;
        }
        
        public async Task TestStudent()
        {
            var count = 0;
            // Base Repository
            //var result = await _studentRepository.Find(14);
            //var result2 = await _studentRepository.Insert(new Student() { FirstMidName = "Carson555", LastName = "Alexander555", EnrollmentDate = DateTime.Parse("2005-09-01") });
            //_studentRepository.Delete(result);
            //result2.FirstMidName = result2.FirstMidName + " updated";
            //result2 = await _studentRepository.Update(result2);
            //var result3 = await _studentRepository.GetAll();

            // Student Repository
            //result.FirstMidName = $"{result.FirstMidName} updated";
            //count = await _studentRepository.UpdateExtended(result);
            //await _studentRepository.Remove(result.ID);
            //count = await _studentRepository.RemoveExtended(result.ID);

            var count2 = await _studentRepository.GetStudentWithEnrollment(2);
        }

        public async Task<Student> TestStudentWithEnrollment()
        {
            return await _studentRepository.GetStudentWithEnrollment(2);
        }
    }
}
