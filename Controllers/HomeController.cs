using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SPA_Practice.Models;

namespace SPA_Practice.Controllers
{
    public class HomeController : Controller
    {
        private readonly ProjectDbContext _db = new ProjectDbContext();

        [HttpGet]
        public ActionResult Index()
        {
            var model = new StudentSubject()
            {
                StudentLookUp = StudentLookUpList(),
            };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Index(StudentSubject model)
        {
            return null;
            if (ModelState.IsValid)
            {
                _db.StudentSubjects.Add(model);
                var isAdded = _db.SaveChanges() > 0;
            }

            StudentSubject studentSubject = new StudentSubject()
            {
                StudentLookUp = StudentLookUpList()
            };

            return View(studentSubject);
        }

        public JsonResult GetTakeableSubjects()
        {
            var datalist = _db.Subjects.ToList();
            return Json(datalist, JsonRequestBehavior.AllowGet);
        }

        public List<SelectListItem> StudentLookUpList()
        {
            var dataList = _db.Students.ToList();
            var selectedStudentList = new List<SelectListItem>();
            selectedStudentList.AddRange(GetDefaultProductSelectedList());
            foreach (var item in dataList)
            {
                var data = new SelectListItem()
                {
                    Value = item.Id.ToString(),
                    Text = item.Name
                };
                selectedStudentList.Add(data);
            }
            return selectedStudentList;
        }

        public List<SelectListItem> GetDefaultProductSelectedList()
        {
            var selectedList = new List<SelectListItem> { new SelectListItem() { Text = "-----------Select-----------", Value = "" } };
            return selectedList;
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Test()
        {
            return View();
        }
    }
}