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
using System.Web.Security;

namespace Lunch.Controllers
{
    public class MenusController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        private Menu CurrentMenu;

        // GET: Menus
        [Authorize(Roles ="admin")]
        public ActionResult Index()
        {
            var menu = db.Menu.Include(m => m.FirstChoice).Include(m => m.SecondChoice);
            return View(menu.ToList());
        }
        public ActionResult ShowMenu()
        {
            CurrentMenu = db.Menu.Include(m => m.FirstChoice).Include(m => m.SecondChoice).ToList().Last();
            ViewBag.MenuStatus = "Our Menu for the day";
            if (CurrentMenu.Date.Day < DateTime.Now.Day)
            {
                CurrentMenu = null; 
                ViewBag.MenuStatus = "Menu has not been set for day, please check back";
            }
         //   var adminId = db.Roles.First().Users.Where(id => id.UserId == User.Identity.GetUserId()).First().UserI
           return View(CurrentMenu);
        }

        // GET: Menus/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        
        // GET: Menus/Create
        [Authorize(Roles ="admin")]
        public ActionResult Create()
        {
            ViewBag.FirstChoiceId = new SelectList(db.Food, "Id", "Name");
            ViewBag.SecondChoiceId = new SelectList(db.Food, "Id", "Name");
            return View();
        }

        // POST: Menus/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "FirstChoiceId,SecondChoiceId")] AddMenuViewModel menu)
        {
            Menu menus = new Menu();
            menus.FirstChoiceId = menu.FirstChoiceId;
            menus.SecondChoiceId = menu.SecondChoiceId;
            menus.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Menu.Add(menus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.FirstChoiceId = new SelectList(db.Food, "Id", "Name", menu.FirstChoiceId);
            ViewBag.SecondChoiceId = new SelectList(db.Food, "Id", "Name", menu.SecondChoiceId);
            return View(menu);
        }

        // GET: Menus/Edit/5
        [Authorize(Roles = "admin")]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            ViewBag.FirstChoiceId = new SelectList(db.Food, "Id", "Name", menu.FirstChoiceId);
            ViewBag.SecondChoiceId = new SelectList(db.Food, "Id", "Name", menu.SecondChoiceId);
            return View(menu);
        }

        // POST: Menus/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize(Roles = "admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,FirstChoiceId,SecondChoiceId")] Menu menu)
        {
            menu.Date = DateTime.Now;
            if (ModelState.IsValid)
            {
                db.Entry(menu).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.FirstChoiceId = new SelectList(db.Food, "Id", "Name", menu.FirstChoiceId);
            ViewBag.SecondChoiceId = new SelectList(db.Food, "Id", "Name", menu.SecondChoiceId);
            return View(menu);
        }

        // GET: Menus/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Menu menu = db.Menu.Find(id);
            if (menu == null)
            {
                return HttpNotFound();
            }
            return View(menu);
        }

        // POST: Menus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Menu menu = db.Menu.Find(id);
            db.Menu.Remove(menu);
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
