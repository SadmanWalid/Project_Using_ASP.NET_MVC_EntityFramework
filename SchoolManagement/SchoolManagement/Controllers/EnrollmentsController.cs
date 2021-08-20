using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    public class EnrollmentsController : Controller
    {
        private SchoolManagementDBEntities db = new SchoolManagementDBEntities();

        // GET: Enrollments
        public ActionResult Index()
        {
            var enrollments = db.Enrollments.Include(e => e.Course).Include(e => e.Student).Include(e => e.Teacher);
            return View(enrollments.ToList());
        }

        // GET: Enrollments/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // GET: Enrollments/Create
        public ActionResult Create()
        {
            ViewBag.courseID = new SelectList(db.Courses, "courseID", "title");
            ViewBag.studentID = new SelectList(db.Students, "studentID", "firstName");
            ViewBag.teacherID = new SelectList(db.Teachers, "teacherID", "firstName");
            return View();
        }

        // POST: Enrollments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "enrollmentID,grade,courseID,studentID,teacherID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Enrollments.Add(enrollment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.courseID = new SelectList(db.Courses, "courseID", "title", enrollment.courseID);
            ViewBag.studentID = new SelectList(db.Students, "studentID", "firstName", enrollment.studentID);
            ViewBag.teacherID = new SelectList(db.Teachers, "teacherID", "firstName", enrollment.teacherID);
            return View(enrollment);
        }

        // GET: Enrollments/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            ViewBag.courseID = new SelectList(db.Courses, "courseID", "title", enrollment.courseID);
            ViewBag.studentID = new SelectList(db.Students, "studentID", "firstName", enrollment.studentID);
            ViewBag.teacherID = new SelectList(db.Teachers, "teacherID", "firstName", enrollment.teacherID);
            return View(enrollment);
        }

        // POST: Enrollments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "enrollmentID,grade,courseID,studentID,teacherID")] Enrollment enrollment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(enrollment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.courseID = new SelectList(db.Courses, "courseID", "title", enrollment.courseID);
            ViewBag.studentID = new SelectList(db.Students, "studentID", "firstName", enrollment.studentID);
            ViewBag.teacherID = new SelectList(db.Teachers, "teacherID", "firstName", enrollment.teacherID);
            return View(enrollment);
        }

        // GET: Enrollments/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Enrollment enrollment = db.Enrollments.Find(id);
            if (enrollment == null)
            {
                return HttpNotFound();
            }
            return View(enrollment);
        }

        // POST: Enrollments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Enrollment enrollment = db.Enrollments.Find(id);
            db.Enrollments.Remove(enrollment);
            db.SaveChanges();
            return RedirectToAction("Index");
        }


        [HttpPost]
        public JsonResult GetStudents(string term)
        {

            var students = db.Students.Select(q => new
            {
                name = q.firstName + " " + q.lastName,
                id = q.studentID
            }).Where(q => q.name.Contains(term));

            return Json(students, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]

        public async Task<JsonResult> AddStudents([Bind(Include ="courseID,studentID")] Enrollment enrolment)
        {
            try
            {
                var IsEnrolled = db.Enrollments.Any(q => q.courseID == enrolment.courseID && q.studentID == enrolment.studentID);

                if(ModelState.IsValid && !IsEnrolled)
                {
                    db.Enrollments.Add(enrolment);
                    await db.SaveChangesAsync();
                    return Json(new { IsSuccess=true, Message="Student Added Successfully", JsonRequestBehavior.AllowGet});
                }

                if (IsEnrolled)
                {
                    return Json(new { IsSuccess = false, Message = "Student Already Enrolled", JsonRequestBehavior.AllowGet });
                }
                else
                    return Json(new { IsSuccess=false, Message="Failed to Add Student", JsonRequestBehavior.AllowGet});

            }
            catch (Exception)
            {

                throw;
            }
        
        }
        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
