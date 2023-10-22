using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Week01.Models;

namespace FIT5032_Week01.Controllers
{
    [Authorize(Roles = "Staff,Admin")]
    public class Mock2Controller : Controller
    {
        private Mock2_Table db = new Mock2_Table();

        // GET: Mock2
        [Authorize(Roles = "Staff,Admin")]
        public ActionResult Index()
        {
            return View(db.Mocks.ToList());
        }

        // GET: Mock2/Details/5
        [Authorize(Roles = "Staff,Admin")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mock mock = db.Mocks.Find(id);
            if (mock == null)
            {
                return HttpNotFound();
            }
            return View(mock);
        }

        // GET: Mock2/Create
        [Authorize(Roles = "Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mock2/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Create([Bind(Include = "id,first_name,last_name,email,gender")] Mock mock)
        {
            if (ModelState.IsValid)
            {
                db.Mocks.Add(mock);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mock);
        }

        // GET: Mock2/Edit/5
        [Authorize(Roles = "Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mock mock = db.Mocks.Find(id);
            if (mock == null)
            {
                return HttpNotFound();
            }
            return View(mock);
        }

        // POST: Mock2/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult Edit([Bind(Include = "id,first_name,last_name,email,gender")] Mock mock)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mock).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mock);
        }

        // GET: Mock2/Delete/5
        [Authorize(Roles = "Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Mock mock = db.Mocks.Find(id);
            if (mock == null)
            {
                return HttpNotFound();
            }
            return View(mock);
        }

        // POST: Mock2/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Mock mock = db.Mocks.Find(id);
            db.Mocks.Remove(mock);
            db.SaveChanges();
            return RedirectToAction("Index");
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
