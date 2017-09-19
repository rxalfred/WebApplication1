using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace ContosoUniversity.Model.CoreTestModel
{
    public class Department
    {
        public int DepartmentId { get; set; }

        [Required, StringLength(50), Column(TypeName = "varchar(50)")]
        public string Name { get; set; }

        public ICollection<Student> Students { get; set; }
    }
}
