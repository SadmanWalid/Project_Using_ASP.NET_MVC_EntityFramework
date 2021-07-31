using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    public class StudentsController : Controller
    {

        private readonly SchoolManagementDBEntities db = new SchoolManagementDBEntities();
        // GET: Students
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }
    }
}