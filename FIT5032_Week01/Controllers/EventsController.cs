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
    [Authorize] // Only authorized users can access this controller
    public class EventsController : Controller
    {
        private Events_Model db = new Events_Model();

        // GET: Events
        public ActionResult Index()
        {
            if (User.IsInRole("Staff") || User.IsInRole("Admin"))
            {
                // Staff and Admin can view all events
                return View(db.Events.ToList());
            }
            else
            {
                // Customers can only view their own events
                string userId = User.Identity.GetUserId();
                var customerEvents = db.Events.Where(e => e.UserId == userId).ToList();
                return View(customerEvents);
            }
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create?date=YYYY-MM-DD
        public ActionResult Create(String date)
        {
            if (null == date)
                return RedirectToAction("Index");
            Event e = new Event();
            DateTime convertedDate = DateTime.Parse(date);
            e.Start = convertedDate;
            e.End = convertedDate;
            return View(e);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Title,Start,End,UserId")] Event @event)
        {

            // Check if the user is authenticated
            if (User.Identity.IsAuthenticated)
            {
                string bookingUserId = User.Identity.GetUserId();
                @event.UserId = bookingUserId;

                // Check if the event is valid and falls within the specified time period
                bool isEventValid = IsEventValid(@event) &&
                                @event.Start.TimeOfDay >= TimeSpan.FromHours(8) &&
                                @event.End.TimeOfDay <= TimeSpan.FromHours(17);

                if (ModelState.IsValid && isEventValid)
                {
                    db.Events.Add(@event);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Event conflicts with existing events or falls outside of the specified time period.");
                }
            }
            else
            {
                // Handle the case where the user is not authenticated
                // You might want to redirect to a login page or display an error message.
            }

            return View(@event);
        }


        // Check if the event is valid (no conflicts and within specified hours)
        private bool IsEventValid(Event @event)
        {
            // Check if the event's start time falls within the specified hours (e.g., between 8 am and 5 pm)
            if (@event.Start.TimeOfDay < TimeSpan.FromHours(8) || @event.End.TimeOfDay > TimeSpan.FromHours(17))
            {
                return false;
            }

            // Query the database to check for conflicting events
            bool hasConflicts = db.Events.Any(e =>
                (@event.Start >= e.Start && @event.Start < e.End) || // Start time conflicts
                (@event.End > e.Start && @event.End <= e.End) ||   // End time conflicts
                (e.Start >= @event.Start && e.End <= @event.End)   // Event fully covers another event
            );

            return !hasConflicts;
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Title,Start,End")] Event @event)
        {
            if (ModelState.IsValid)
            {
                db.Entry(@event).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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