using MVC_PROJECT_1278941.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;

namespace MVC_PROJECT_1278941.Controllers
{
    public class DepartmentsController : Controller
    {
        private readonly ApplicantDbContext db = new ApplicantDbContext();
        // GET: Courses
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Create()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Create(Department model)
        {
            if (ModelState.IsValid)
            {
                db.Departments.Add(model);
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }
        public ActionResult Edit(int id)
        {
            var data = db.Departments.FirstOrDefault(x => x.DepartmentId == id);
            if (data == null) return new HttpNotFoundResult();
            return View(data);
        }
        [HttpPost]
        public ActionResult Edit(Department model)
        {
            if (ModelState.IsValid)
            {
                db.Entry(model).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return PartialView("_Success");
            }
            return PartialView("_Fail");
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var course = db.Departments.FirstOrDefault(x => x.DepartmentId == id);
            if (course == null) return new HttpNotFoundResult();
            db.Departments.Remove(course);
            db.SaveChanges();
            return Json(new { success = true });
        }

        public PartialViewResult ModelsTable(int pg = 1)
        {
            var data = db.Departments.OrderBy(x => x.DepartmentId).ToPagedList(pg, 5);
            return PartialView("_Departments", data);
        }
    }
}