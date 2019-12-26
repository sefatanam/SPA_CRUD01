using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Transactions;
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
            //return null;
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

        public JsonResult GetSelectedStudentSubjects(int id)
        {
            var data = _db.StudentSubjects.Where(c => c.StudentId == id).ToList();
            var jsonData = data.Select(c => new { c.SubjectId });
            return Json(jsonData, JsonRequestBehavior.AllowGet);
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

        public bool StudentSubjectAddOrUpdate(List<StudentSubject> StudentSubjects)
        {
            var isAddOrUpdated = false;

            if (StudentSubjects != null && StudentSubjects.Any())
            {
                // Find ProductId List
                var studentIds = StudentSubjects.Select(c => c.StudentId).Distinct().ToList();

                // Get Those Product from Db With a Single Query

                var updateableStudentSubjects = _db.StudentSubjects.Where(c => studentIds.Contains(c.StudentId)).ToList();

                //Update Old StudentSubject Qty
                foreach (var oldStudentSubject in updateableStudentSubjects)
                {
                    oldStudentSubject.SubjectId += StudentSubjects.Where(c => c.StudentId == oldStudentSubject.StudentId)?.Sum(c => c.SubjectId) ?? 0;
                }

                //Find New StudentSubject Products
                var oldStudentIds = updateableStudentSubjects.Select(c => c.StudentId).Distinct().ToList();
                var newStudentIds = studentIds.Where(c => !oldStudentIds.Contains(c));

                //find New Addable Items
                var addableStudentSubject = new List<StudentSubject>();

                foreach (var stuId in newStudentIds)
                {
                    var stuSub = new StudentSubject()
                    {
                        SubjectId = StudentSubjects.Where(c => c.StudentId == stuId)?.Sum(c => c.SubjectId) ?? 0,
                        StudentId = stuId
                    };
                    addableStudentSubject.Add(stuSub);
                }

                //Use Transaction Scope For Holding Database
                using (var ts = new TransactionScope())
                {
                    _db.StudentSubjects.AddRange(addableStudentSubject);

                    foreach (var updateableStudentSubject in updateableStudentSubjects)
                    {
                        _db.StudentSubjects.AddOrUpdate(updateableStudentSubject);
                    }

                    isAddOrUpdated = _db.SaveChanges() > 0;

                    if (isAddOrUpdated)
                    {
                        ts.Complete();
                    }
                }

                //foreach (var StudentSubject in StudentSubjects)
                //{
                //    var olf = _db.StudentSubjects.FirstOrDefault(c => c.ProductId == StudentSubject.ProductId);
                //}
            }

            return isAddOrUpdated;
        }
    }
}