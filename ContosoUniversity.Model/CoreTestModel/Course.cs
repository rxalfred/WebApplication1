using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ContosoUniversity.Model.CoreTestModel
{
    public class Course
    {
        public int CourseID { get; set; }

        [Required, StringLength(50), Column(TypeName = "varchar(50)")]
        public string Title { get; set; }

        public int Credits { get; set; }

        [Column(TypeName = "decimal(18, 6)")]
        public decimal Amount { get; set; }

        public ICollection<Enrollment> Enrollments { get; set; }
    }
}
