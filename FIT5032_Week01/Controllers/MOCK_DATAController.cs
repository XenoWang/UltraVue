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
    [Authorize(Roles = "Staff,Admin")] // Restrict access to Staff and Admin roles
    public class MOCK_DATAController : Controller
    {
        private Mock_Data_Model db = new Mock_Data_Model();

        // GET: MOCK_DATA
        [Authorize(Roles = "Staff,Admin")] // Only Staff can read
        public ActionResult Index()
        {
            return View(db.MOCK_DATA.ToList());
        }

        // GET: MOCK_DATA/Details/5
        [Authorize(Roles = "Staff,Admin")] // Only Staff can view details
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOCK_DATA mOCK_DATA = db.MOCK_DATA.Find(id);
            if (mOCK_DATA == null)
            {
                return HttpNotFound();
            }
            return View(mOCK_DATA);
        }

        // GET: MOCK_DATA/Create
        [Authorize(Roles = "Admin")] // Only Admin can create
        public ActionResult Create()
        {
            return View();
        }

        // POST: MOCK_DATA/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Only Admin can create
        public ActionResult Create([Bind(Include = "id,first_name,last_name,email,gender,ip_address")] MOCK_DATA mOCK_DATA)
        {
            if (ModelState.IsValid)
            {
                db.MOCK_DATA.Add(mOCK_DATA);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(mOCK_DATA);
        }

        // GET: MOCK_DATA/Edit/5
        [Authorize(Roles = "Admin")] // Only Admin can edit
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOCK_DATA mOCK_DATA = db.MOCK_DATA.Find(id);
            if (mOCK_DATA == null)
            {
                return HttpNotFound();
            }
            return View(mOCK_DATA);
        }

        // POST: MOCK_DATA/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Only Admin can edit
        public ActionResult Edit([Bind(Include = "id,first_name,last_name,email,gender,ip_address")] MOCK_DATA mOCK_DATA)
        {
            if (ModelState.IsValid)
            {
                db.Entry(mOCK_DATA).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(mOCK_DATA);
        }

        // GET: MOCK_DATA/Delete/5
        [Authorize(Roles = "Admin")] // Only Admin can delete
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            MOCK_DATA mOCK_DATA = db.MOCK_DATA.Find(id);
            if (mOCK_DATA == null)
            {
                return HttpNotFound();
            }
            return View(mOCK_DATA);
        }

        // POST: MOCK_DATA/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Admin")] // Only Admin can delete
        public ActionResult DeleteConfirmed(int id)
        {
            MOCK_DATA mOCK_DATA = db.MOCK_DATA.Find(id);
            db.MOCK_DATA.Remove(mOCK_DATA);
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

