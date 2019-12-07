using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using SPA_Practice.Models;

namespace SPA_Practice
{
    public class ProjectDbContext : DbContext
    {
        public ProjectDbContext() : base("ProjectDbContext")
        {
        }

        public DbSet<Student> Students { get; set; }
        public DbSet<Subject> Subjects { get; set; }
        public DbSet<StudentSubject> StudentSubjects { get; set; }
    }
}