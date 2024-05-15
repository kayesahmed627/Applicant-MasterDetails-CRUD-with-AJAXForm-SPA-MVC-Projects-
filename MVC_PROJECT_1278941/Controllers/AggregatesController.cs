using MVC_PROJECT_1278941.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;

namespace MVC_PROJECT_1278941.Controllers
{
    public class AggregatesController : Controller
    {
        private readonly ApplicantDbContext db = new ApplicantDbContext();
        // GET: Aggregates
        public ActionResult Index()
        {
            var data = db.Applicants.Include(x =>x.Qualifications).ToList();
            return View(data);
        }
    }
}