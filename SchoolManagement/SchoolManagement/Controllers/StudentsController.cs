using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    [Authorize]
    public class StudentsController : Controller
    {

        private readonly SchoolManagementDBEntities db = new SchoolManagementDBEntities();
        // GET: Students
        [AllowAnonymous]
        public ActionResult Index()
        {
            return View(db.Students.ToList());
        }

        // GET : Students/Details/5

        public ActionResult Details(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if(student==null)
            {
                return HttpNotFound();
            }
            return View(student); 
        }

        //GET : Students/Create

        public ActionResult Create()
        {

            return View();

        }

        // POST : Students/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public ActionResult Create([Bind(Include ="firstName,lastName,enrollmentDate,middleName")]Student student) //inlcude is non case sensitive
        {
            if(ModelState.IsValid)
            {
                db.Students.Add(student);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);

        }
        //GET : Students/Edit/5

        public ActionResult Edit(int? id)
        {

            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest );
            }
            Student student = db.Students.Find(id);
            if(student==null)
            {
                return HttpNotFound();
            }
            return View(student);

        }

        //POST : Students/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include ="studentID,firstName,lastName,enrollmentDate,middleName")] Student student)
        {
            if(ModelState.IsValid)
            {
                db.Entry(student).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(student);

        }

        //Get : Students/Delete/5

        public ActionResult Delete(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Student student = db.Students.Find(id);
            if(student==null)
            {
                return HttpNotFound();
            }
            return View(student);
        }

        //Post : Students/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirme(int id)
        {
            Student student = db.Students.Find(id);
            db.Students.Remove(student);
            db.SaveChanges();
            return RedirectToAction("Index");

        }
        protected override void Dispose(bool disposing)
        {
            if(disposing)
            { 
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}