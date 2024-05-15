using MVC_PROJECT_1278941.Models;
using MVC_PROJECT_1278941.ViewModel.InputModel;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using System.Data.Entity;
using System.Threading;

namespace MVC_PROJECT_1278941.Controllers
{
    [Authorize(Roles = "Admin, Members")]
    public class ApplicantsController : Controller
    {
        private readonly ApplicantDbContext db = new ApplicantDbContext();
        // GET: Students
        public ActionResult Index()
        {
            return View();
        }
        public PartialViewResult ApplicantTable(int pg = 1)
        {
            var data = db.Applicants
                .Include(x => x.Qualifications)
                .Include(x => x.Department)
                .OrderBy(x => x.ApplicantId)
                .ToPagedList(pg, 5);
            Thread.Sleep(1000);
            return PartialView("_ApplicantTable", data);
        }
       [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }
        public ActionResult CreateForm()
        {
            ApplicantInputModel model = new ApplicantInputModel();
            model.Qualifications.Add(new Qualification());
            ViewBag.Departments = db.Departments.ToList();
            return PartialView("_CreateForm", model);
        }

        [HttpPost]
        public ActionResult Create(ApplicantInputModel model, string act = "")
        {
            if (act == "add")
            {
                model.Qualifications.Add(new Qualification());
                foreach (var v in ModelState.Values)
                {
                    v.Errors.Clear();
                    v.Value = null;
                }
            }
            if (act.StartsWith("remove"))
            {
                var index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                model.Qualifications.RemoveAt(index);

                foreach (var v in ModelState.Values)
                {
                    v.Errors.Clear();
                    v.Value = null;
                }
            }
            if (act == "insert")
            {
                if (ModelState.IsValid)
                {
                    var applicant = new Applicant
                    {
                        ApplicantName = model.ApplicantName,
                        Phone = model.Phone,
                        BirthDate = model.BirthDate,
                        PayRate = model.PayRate,
                        DepartmentId = model.DepartmentId,

                    };
                    string ext = Path.GetExtension(model.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Path.Combine(Server.MapPath("~/Pictures"), fileName);
                    model.Picture.SaveAs(savePath);
                    applicant.Picture = fileName;

                    db.Applicants.Add(applicant);
                    db.SaveChanges();
                    foreach (var s in model.Qualifications)
                    {
                        db.Database.ExecuteSqlCommand($@"EXEC InsertQualification '{s.Institute}', {s.PassingYear}, {(int)s.Degree}, {applicant.ApplicantId}");
                    }
                    ApplicantInputModel newmodel = new ApplicantInputModel() { ApplicantName = "" };
                    newmodel.Qualifications.Add(new Qualification());
                    ViewBag.Departments = db.Departments.ToList();

                    foreach (var e in ModelState.Values)
                    {
                        e.Value = null;
                    }
                    return View("_CreateForm", newmodel);
                }
            }
            ViewBag.Departments = db.Departments.ToList();
            return View("_CreateForm", model);
        }

       [Authorize(Roles = "Admin")]
        public ActionResult Edit(int id)
        {
            ViewBag.Id = id;
            return View();
        }

        public ActionResult EditForm(int id)
        {
            var s = db.Applicants.FirstOrDefault(x => x.ApplicantId == id);
            if (s == null) return new HttpNotFoundResult();
            db.Entry(s).Collection(x => x.Qualifications).Load();
            ApplicantEditModel model = new ApplicantEditModel
            {
                ApplicantId = id,
                DepartmentId = s.DepartmentId,
                ApplicantName = s.ApplicantName,
                Phone = s.Phone,
                BirthDate = s.BirthDate,
                PayRate = s.PayRate,
                Qualifications=s.Qualifications.ToList(),
            };
           // model.Qualifications = s.Qualifications.ToList();
            ViewBag.CurrentPic = s.Picture;
            ViewBag.Departments = db.Departments.ToList();
            return PartialView("_EditForm", model);
        }
        [HttpPost]
        public ActionResult Edit(ApplicantEditModel model, string act = "")
        {
            //var applicant = db.Applicants.FirstOrDefault(x => x.ApplicantId == model.ApplicantId);
            //if (applicant == null) return new HttpNotFoundResult();

            if (act == "add")
            {
                model.Qualifications.Add(new Qualification());
                foreach (var v in ModelState.Values)
                {
                    v.Errors.Clear();
                    v.Value = null;
                }
            }
            if (act.StartsWith("remove"))
            {
                var index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                model.Qualifications.RemoveAt(index);
                foreach (var v in ModelState.Values)
                {
                    v.Errors.Clear();
                    v.Value = null;
                }
            }
            if (act == "update")
            {
                if (ModelState.IsValid)
                {
                    var applicant = db.Applicants.FirstOrDefault(x => x.ApplicantId == model.ApplicantId);
                    if (applicant == null) { return new HttpNotFoundResult(); }
                    applicant.ApplicantName = model.ApplicantName;
                    applicant.Phone = model.Phone;
                    applicant.BirthDate = model.BirthDate;
                    applicant.PayRate = model.PayRate;
                    applicant.DepartmentId = model.DepartmentId;
                    if (model.Picture != null)
                    {
                        string ext = Path.GetExtension(model.Picture.FileName);
                        string f = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string savePath = Path.Combine(Server.MapPath("~/Pictures"), f);
                        model.Picture.SaveAs(savePath);
                        applicant.Picture = f;
                    }

                    db.SaveChanges();
                    db.Database.ExecuteSqlCommand($"EXEC DeleteQualificationByApplicantId {applicant.ApplicantId}");
                    foreach (var s in model.Qualifications)
                    {
                        db.Database.ExecuteSqlCommand($@"EXEC InsertQualification '{s.Institute}', {s.PassingYear}, {(int)s.Degree}, {applicant.ApplicantId}");
                    }
                }
            }
            ViewBag.Departments = db.Departments.ToList();
            ViewBag.CurrentPic = db.Applicants.FirstOrDefault(x => x.ApplicantId == model.ApplicantId)?.Picture;
            ViewBag.DateTime = db.Applicants.FirstOrDefault(x => x.ApplicantId == model.ApplicantId)?.BirthDate;
            return View("_EditForm", model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var applicant = db.Applicants.FirstOrDefault(x => x.ApplicantId == id);
            if (applicant == null) return new HttpNotFoundResult();
            db.Qualifications.RemoveRange(applicant.Qualifications.ToList());
            db.Applicants.Remove(applicant);
            db.SaveChanges();
            return Json(new { success = true });
        }

    }
}