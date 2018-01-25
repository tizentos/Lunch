using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Lunch.Models;
using Microsoft.AspNet.Identity;
using System.Collections;

namespace Lunch.Controllers
{
    [Authorize]
    public class BookingsController : Controller
    {
        public ApplicationDbContext db = new ApplicationDbContext();
        private Menu CurrentMenu;
        // GET: Bookings
        [Authorize(Roles = "admin")]
        public ActionResult Index()
        {
            var booking = db.Booking
                .Where(bookings => bookings.Date.Day == DateTime.Now.Day)
                .Include(b => b.FoodChoice);
            return View(booking.ToList());
        }
        public ActionResult Mybookings()
        {
            var userIdInSession = User.Identity.GetUserId();
            ApplicationUser UserInSession = db.Users.Where(user => user.Id == userIdInSession).First();
            var booking = db.Booking
                .Where(mybooking => mybooking.User.Id == UserInSession.Id)
                .Include(b => b.FoodChoice).ToList();
            return View(booking.ToList());
        }

        [Authorize(Roles ="admin")]
        public ActionResult BookingHistory()
        {
            var booking = db.Booking
                .Include(b => b.FoodChoice);
            return View(booking.ToList());
        }
        // GET: Bookings/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Booking.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // GET: Bookings/Create
        public ActionResult Create()
        {
            CurrentMenu = db.Menu.Include(m => m.FirstChoice).Include(m => m.SecondChoice).ToList().Last();
            IList MenuFoodList;
            //15 Minute logic
            if (CurrentMenu.Date.Minute + 15 >= DateTime.Now.Minute)
            {
                MenuFoodList = new List<Food>() { CurrentMenu.FirstChoice, CurrentMenu.SecondChoice };
                ViewBag.FoodChoiceId = new SelectList(MenuFoodList, "Id", "Name");
                ViewBag.Status = "intime";
            }
            else
            {
                MenuFoodList = new List<Food>();
                ViewBag.FoodChoiceId = new SelectList(MenuFoodList, "Id", "Name");
                ViewBag.Status = "Menu has not been set, check back later";
            }
            return View();
        }

        // POST: Bookings/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FoodChoiceId,DeliveryLocation,CustomMessage")] AddBookingViewModel booking)
        {
            var userIdInSession = User.Identity.GetUserId();
            ApplicationUser UserInSession = db.Users.Where(user => user.Id == userIdInSession).First();
            Booking bookings = new Booking();
            bookings.Date = DateTime.Now;
            bookings.FoodChoiceId = booking.FoodChoiceId;
            bookings.DeliveryLocation = booking.DeliveryLocation;
            bookings.CustomMessage = booking.CustomMessage;
            bookings.User = UserInSession;

            if (ModelState.IsValid)
            {
                db.Booking.Add(bookings);
                db.SaveChanges();
                return RedirectToAction("Mybookings");
            }

            CurrentMenu = db.Menu.Include(m => m.FirstChoice).Include(m => m.SecondChoice).ToList().Last();
            List<Food> MenuFoodList = new List<Food>() { CurrentMenu.FirstChoice, CurrentMenu.SecondChoice };
            ViewBag.FoodChoiceId = new SelectList(MenuFoodList, "Id", "Name");
            return View(booking);
        }

        // GET: Bookings/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Booking.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            ViewBag.FoodChoiceId = new SelectList(db.Food, "Id", "Name", booking.FoodChoiceId);
            return View(booking);
        }

        // POST: Bookings/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FoodChoiceId,Date,DeliveryLocation,CustomMessage")] Booking booking)
        {
            if (ModelState.IsValid)
            {
                db.Entry(booking).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FoodChoiceId = new SelectList(db.Food, "Id", "Name", booking.FoodChoiceId);
            return View(booking);
        }

        // GET: Bookings/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Booking booking = db.Booking.Find(id);
            if (booking == null)
            {
                return HttpNotFound();
            }
            return View(booking);
        }

        // POST: Bookings/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Booking booking = db.Booking.Find(id);
            db.Booking.Remove(booking);
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
