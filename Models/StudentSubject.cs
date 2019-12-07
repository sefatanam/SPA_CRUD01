using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SPA_Practice.Models
{
    public class StudentSubject
    {
        public int Id { get; set; }
        public int StudentId { get; set; }

        [NotMapped]
        public virtual Student Student { get; set; }

        public int SubjectId { get; set; }

        public virtual ICollection<Subject> Subjects { get; set; }

        [NotMapped]
        public virtual Subject Subject { get; set; }

        [NotMapped]
        public ICollection<SelectListItem> StudentLookUp { get; set; }

        //public virtual ICollection<Subject> Subject { get; set; }
    }
}