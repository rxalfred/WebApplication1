using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContosoUniversity.Model.CoreTestModel
{
    public class SchoolContext : DbContext
    {
        public SchoolContext(DbContextOptions<SchoolContext> options) : base(options)
        {
        }

        public DbSet<Course> Courses { get; set; }
        public DbSet<Enrollment> Enrollments { get; set; }
        public DbSet<Student> Students { get; set; }
        public DbSet<Department> Departments { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Course>().ToTable("Course");
            modelBuilder.Entity<Enrollment>().ToTable("Enrollment");
            modelBuilder.Entity<Student>().ToTable("Student");
            modelBuilder.Entity<Department>().ToTable("Department");

            //modelBuilder.Entity<Course>()
            //    .Property(e => e.Title)
            //    .IsUnicode(false);

            // if ID can be null ( default EF will delete another if ID not null ) 
            // public int? InstructorID { get; set; }
            //modelBuilder.Entity<Department>()
            //.HasOne(d => d.Administrator)
            //.WithMany()
            //.OnDelete(DeleteBehavior.Restrict)
        }
    }
}