using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ContosoUniversity.Model.CoreTestModel;
using ContosoUniversity.BusinessLogic.Logic;

namespace WebApplication2.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;
        private readonly IStudentLogic _studentLogic;

        public StudentsController(SchoolContext context, IStudentLogic studentLogic)
        {
            _context = context;
            _studentLogic = studentLogic;
        }

        // GET: Students
        public async Task<IActionResult> Index()
        {
            await _studentLogic.TestStudent();
            //var viewModel = new InstructorIndexData();
            //viewModel.Instructors = await _context.Instructors
            //      .Include(i => i.OfficeAssignment)
            //      .Include(i => i.CourseAssignments)
            //        .ThenInclude(i => i.Course)
            //            .ThenInclude(i => i.Enrollments)
            //                .ThenInclude(i => i.Student)
            //      .Include(i => i.CourseAssignments)
            //        .ThenInclude(i => i.Course)
            //            .ThenInclude(i => i.Department)
            //      .AsNoTracking()
            //      .OrderBy(i => i.LastName)
            //      .ToListAsync();

            //if (id != null)
            //{
            //    ViewData["InstructorID"] = id.Value;
            //    Instructor instructor = viewModel.Instructors.Where(i => i.ID == id.Value).Single();
            //    viewModel.Courses = instructor.CourseAssignments.Select(s => s.Course);
            //}

            //if (courseID != null)
            //{
            //    ViewData["CourseID"] = courseID.Value;
            //    viewModel.Enrollments = viewModel.Courses.Where(x => x.CourseID == courseID).Single().Enrollments;

            //    var selectedCourse = viewModel.Courses.Where(x => x.CourseID == courseID).Single();
            //    await _context.Entry(selectedCourse).Collection(x => x.Enrollments).LoadAsync();
            //    foreach (Enrollment enrollment in selectedCourse.Enrollments)
            //    {
            //        await _context.Entry(enrollment).Reference(x => x.Student).LoadAsync();
            //    }
            //    viewModel.Enrollments = selectedCourse.Enrollments;
            //}

            return View(await _context.Students.ToListAsync());
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .SingleOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // GET: Students/Create
        public IActionResult Create()
        {
            PopulateDepartmentsDropDownList();
            return View();
        }

        // POST: Students/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LastName,FirstMidName,EnrollmentDate,DepartmentId")] Student student)
        {
            if (ModelState.IsValid)
            {
                var course = new Course()
                { Title = "Chemistry Advance", Credits = 4, Amount = 0 };

                var enrollment = new Enrollment()
                { Grade = Grade.B};
                //{ StudentID = student.ID, CourseID = course.CourseID,Grade = Grade.B};

                enrollment.Course = course;
                enrollment.Student = student;
                //_context.Add(course);
                //_context.Add(student);
                _context.Add(enrollment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList(student.DepartmentId);
            return View(student);
        }

        // GET: Students/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students.SingleOrDefaultAsync(m => m.ID == id);
            if (student == null)
            {
                return NotFound();
            }

            PopulateDepartmentsDropDownList(student.DepartmentId);

            return View(student);
        }

        // POST: Students/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LastName,FirstMidName,EnrollmentDate,DepartmentId")] Student student)
        {
            if (id != student.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(student);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!StudentExists(student.ID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            PopulateDepartmentsDropDownList(student.DepartmentId);
            return View(student);
        }

        // GET: Students/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var student = await _context.Students
                .Include(c => c.Department)
                .AsNoTracking()
                .SingleOrDefaultAsync(m => m.ID == id);

            if (student == null)
            {
                return NotFound();
            }

            return View(student);
        }

        // POST: Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var student = await _context.Students.SingleOrDefaultAsync(m => m.ID == id);
            _context.Students.Remove(student);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool StudentExists(int id)
        {
            return _context.Students.Any(e => e.ID == id);
        }

        private void PopulateDepartmentsDropDownList(object selectedDepartment = null)
        {
            var departmentsQuery = from d in _context.Departments
                                   orderby d.Name
                                   select d;
            ViewBag.DepartmentID = new SelectList(departmentsQuery.AsNoTracking(), "DepartmentId", "Name", selectedDepartment);
        }
    }
}
