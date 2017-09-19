using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class Enrollment
    {
        public int EnrollmentId { get; set; }
        public int CourseId { get; set; }
        public int? Grade { get; set; }
        public int StudentId { get; set; }

        public Course Course { get; set; }
        public Student Student { get; set; }
    }
}
