using MVC_PROJECT_1278941.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using MVC_PROJECT_1278941.ViewModel;

namespace MVC_PROJECT_1278941.Controllers
{
    public class GroupingsController : Controller
    {
        private readonly ApplicantDbContext db = new ApplicantDbContext();
        // GET: Groupings
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult GroupingByName()
        {
            var data = db.Qualifications.Include(x=>x.Applicant).ToList()
               .GroupBy(s => s.Applicant.ApplicantName)
               .Select(g => new GroupedData { Key = g.Key, Data = g.Select(x => x) })
               .ToList();
            return View(data);
        }
        public ActionResult GroupingByDegree()
        {
            var data = db.Qualifications
                .ToList()            
               .GroupBy(s => s.Degree)
               .Select(g => new GroupedData { Key = g.Key.ToString(), Data = g.Select(x => x) })
               .ToList();
            return View(data);
        }

    }
}