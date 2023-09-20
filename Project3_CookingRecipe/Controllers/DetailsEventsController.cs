using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Project3_CookingRecipe.Models;

namespace Project3_CookingRecipe.Controllers
{
    public class DetailsEventsController : Controller
    {
        private DatabaseCookingJameEntities1 db = new DatabaseCookingJameEntities1();

        // GET: DetailsEvents
        public ActionResult Index()
        {
            var detailsEvents = db.DetailsEvents.Include(d => d.CookingRecipe).Include(d => d.Event).Include(d => d.User);
            return View(detailsEvents.ToList());
        }

        // GET: DetailsEvents/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsEvent detailsEvent = db.DetailsEvents.Find(id);
            if (detailsEvent == null)
            {
                return HttpNotFound();
            }
            return View(detailsEvent);
        }

        // GET: DetailsEvents/Create
        public ActionResult Create()
        {
            ViewBag.IdRecipe = new SelectList(db.CookingRecipes, "IdRecipe", "NameFood");
            ViewBag.IdEvent = new SelectList(db.Events, "IdEvent", "NameEvent");
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "FullName");
            return View();
        }

        // POST: DetailsEvents/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEvent,IdUser,IdRecipe")] DetailsEvent detailsEvent)
        {
            if (ModelState.IsValid)
            {
                db.DetailsEvents.Add(detailsEvent);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdRecipe = new SelectList(db.CookingRecipes, "IdRecipe", "NameFood", detailsEvent.IdRecipe);
            ViewBag.IdEvent = new SelectList(db.Events, "IdEvent", "NameEvent", detailsEvent.IdEvent);
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "FullName", detailsEvent.IdUser);
            return View(detailsEvent);
        }

        // GET: DetailsEvents/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsEvent detailsEvent = db.DetailsEvents.Find(id);
            if (detailsEvent == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdRecipe = new SelectList(db.CookingRecipes, "IdRecipe", "NameFood", detailsEvent.IdRecipe);
            ViewBag.IdEvent = new SelectList(db.Events, "IdEvent", "NameEvent", detailsEvent.IdEvent);
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "FullName", detailsEvent.IdUser);
            return View(detailsEvent);
        }

        // POST: DetailsEvents/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEvent,IdUser,IdRecipe")] DetailsEvent detailsEvent)
        {
            if (ModelState.IsValid)
            {
                db.Entry(detailsEvent).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdRecipe = new SelectList(db.CookingRecipes, "IdRecipe", "NameFood", detailsEvent.IdRecipe);
            ViewBag.IdEvent = new SelectList(db.Events, "IdEvent", "NameEvent", detailsEvent.IdEvent);
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "FullName", detailsEvent.IdUser);
            return View(detailsEvent);
        }

        // GET: DetailsEvents/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DetailsEvent detailsEvent = db.DetailsEvents.Find(id);
            if (detailsEvent == null)
            {
                return HttpNotFound();
            }
            return View(detailsEvent);
        }

        // POST: DetailsEvents/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            DetailsEvent detailsEvent = db.DetailsEvents.Find(id);
            db.DetailsEvents.Remove(detailsEvent);
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
