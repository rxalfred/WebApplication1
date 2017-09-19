using System;
using System.Collections.Generic;

namespace WebApplication2.Models
{
    public partial class Course
    {
        public Course()
        {
            Enrollment = new HashSet<Enrollment>();
        }

        public int CourseId { get; set; }
        public int Credits { get; set; }
        public string Title { get; set; }

        public ICollection<Enrollment> Enrollment { get; set; }
    }
}
