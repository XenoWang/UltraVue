using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using FIT5032_Week01.Models;
using Microsoft.AspNet.Identity;

namespace FIT5032_Week01.Controllers
{
    [Authorize]
    public class CommentsController : Controller
    {
        private Comment_Model db = new Comment_Model();

        // GET: Comments
        [Authorize(Roles = "Staff,Admin,Customer")]
        public ActionResult Index()
        {
            if (User.IsInRole("Admin") || User.IsInRole("Staff"))
            {
                return View(db.Comments.ToList());
            }
            else if (User.IsInRole("Customer"))
            {
                string currentUserId = User.Identity.GetUserId();
                var customerComments = db.Comments.Where(c => c.UserId == currentUserId).ToList();
                return View(customerComments);
            }

            return View();
        }

        // GET: Comments/Details/5
        [Authorize(Roles = "Staff,Admin,Customer")]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            string currentUserId = User.Identity.GetUserId();
            if (User.IsInRole("Customer") && comment.UserId != currentUserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(comment);
        }

        // GET: Comments/Create
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult Create()
        {
            return View();
        }

        // POST: Comments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult Create([Bind(Include = "Id,UserId,Rating,Content")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                string currentUserId = User.Identity.GetUserId();
                comment.UserId = currentUserId; // Set the comment's UserId to the current user
                db.Comments.Add(comment);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(comment);
        }

        // GET: Comments/Edit/5
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            string currentUserId = User.Identity.GetUserId();
            if (User.IsInRole("Customer") && comment.UserId != currentUserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(comment);
        }

        // POST: Comments/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult Edit([Bind(Include = "Id,UserId,Rating,Content")] Comment comment)
        {
            if (ModelState.IsValid)
            {
                db.Entry(comment).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(comment);
        }

        // GET: Comments/Delete/5
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Comment comment = db.Comments.Find(id);
            if (comment == null)
            {
                return HttpNotFound();
            }

            string currentUserId = User.Identity.GetUserId();
            if (User.IsInRole("Customer") && comment.UserId != currentUserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            return View(comment);
        }

        // POST: Comments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Customer,Admin")]
        public ActionResult DeleteConfirmed(int id)
        {
            Comment comment = db.Comments.Find(id);

            string currentUserId = User.Identity.GetUserId();
            if (User.IsInRole("Customer") && comment.UserId != currentUserId)
            {
                return new HttpStatusCodeResult(HttpStatusCode.Forbidden);
            }

            db.Comments.Remove(comment);
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
