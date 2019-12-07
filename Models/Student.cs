using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SPA_Practice.Models
{
    public class Student
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}