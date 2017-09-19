using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContosoUniversity.Model.CoreTestModel
{
    public class Student
    {
        public int ID { get; set; }

        [StringLength(50), Required]
        //[StringLength(50, MinimumLength = 1)]
        public string LastName { get; set; }

        [StringLength(50), Required]
        //[StringLength(50, ErrorMessage = "First name cannot be longer than 50 characters.")]
        public string FirstMidName { get; set; }

        [Display(Name = "Full Name")]
        public string FullName
        {
            get
            {
                return LastName + ", " + FirstMidName;
            }
        }

        public int DepartmentId { get; set; }
        //[Range(0, 5)] for int

        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [DataType(DataType.Date), Display(Name = "Enrollment Date"), DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime EnrollmentDate { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
        public Department Department { get; set; }
    }
}