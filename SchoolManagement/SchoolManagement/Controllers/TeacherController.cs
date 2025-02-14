﻿using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SchoolManagement.Models;

namespace SchoolManagement.Controllers
{
    public class TeacherController : Controller
    {
        private readonly SchoolManagementDBEntities db = new SchoolManagementDBEntities();

        // GET: Teacher
        public ActionResult Index()
        {
            return View(db.Teachers.ToList());
        }
        //GET : Teacher/Details/5

        public ActionResult Details(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher Teacher = db.Teachers.Find(id);
            if(Teacher==null)
            {
                return HttpNotFound();
            }

            return View(Teacher);

        }

        //GET : Teacher/Create

        public ActionResult Create()
        {

            return View();
        }

        //POST : Teacher/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
       public ActionResult Create([Bind(Include ="firstName,lastName")] Teacher Teacher )
        {
            if(ModelState.IsValid)
            {
                db.Teachers.Add(Teacher);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Teacher);
        }


        //GET : Teacher/Edit/5
        public ActionResult Edit(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher teacher = db.Teachers.Find(id);
            if(teacher==null)
            {
                return HttpNotFound();
            }
            return View(teacher);
        }

        //POST : Teacher/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Edit([Bind(Include ="firstName,lastName,teacherID")] Teacher Teacher)
        {
            if(ModelState.IsValid)
            {
                db.Entry(Teacher).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(Teacher);
        }

        //GET : Teacher/Delete/5

        public ActionResult Delete(int? id)
        {
            if(id==null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Teacher Teacher = db.Teachers.Find(id);
            if (Teacher == null)
            {
                return HttpNotFound();
            }

            return View(Teacher);

        }

        //POST : Teacher/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]

        public ActionResult Delete(int id)
        {
            Teacher Teacher = db.Teachers.Find(id);
            db.Teachers.Remove(Teacher);
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