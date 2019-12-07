using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace SPA_Practice.Models
{
    public class Subject
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsOptional { get; set; }

        public virtual ICollection<StudentSubject> StudentSubjects { get; set; }
    }
}