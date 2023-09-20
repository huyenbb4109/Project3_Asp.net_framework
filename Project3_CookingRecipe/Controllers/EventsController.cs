using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PagedList;
using Project3_CookingRecipe.Models;

namespace Project3_CookingRecipe.Controllers
{
    public class EventsController : Controller
    {
        private DatabaseCookingJameEntities1 db = new DatabaseCookingJameEntities1();

        // GET: Events
        public ActionResult Index(string sortOrder, string currentFilter, string searchString, int? page, int? size)
        {
            //phan trang theo dropdown
            List<SelectListItem> items = new List<SelectListItem>();
            items.Add(new SelectListItem { Text = "5", Value = "5" });
            items.Add(new SelectListItem { Text = "10", Value = "10" });
            items.Add(new SelectListItem { Text = "20", Value = "20" });
            items.Add(new SelectListItem { Text = "25", Value = "25" });
            items.Add(new SelectListItem { Text = "50", Value = "50" });
            items.Add(new SelectListItem { Text = "100", Value = "100" });
            items.Add(new SelectListItem { Text = "200", Value = "200" });

            // 1.1. Giữ trạng thái kích thước trang được chọn trên DropDownList
            foreach (var item in items)
            {
                if (item.Value == size.ToString()) item.Selected = true;
            }

            // 1.2. Tạo các biến ViewBag
            ViewBag.size = items; // ViewBag DropDownList
            ViewBag.currentSize = size; // tạo biến kích thước trang hiện tại


            //=====================sắp xếp============================================
            ViewBag.CurrentSort = sortOrder;
            ViewBag.NameSortParm = String.IsNullOrEmpty(sortOrder) ? "name_desc" : "";
            ViewBag.StartTime = sortOrder == "date" ? "date_desc" : "date";
            ViewBag.EndTime = sortOrder == "dateEnd" ? "dateEnd_desc" : "dateEnd";
            //ViewBag.FullName = sortOrder == "fullname" ? "fullname_desc" : "fullname";

            if (searchString != null)
            {
                page = 1;
            }
            else
            {
                searchString = currentFilter;
            }

            ViewBag.CurrentFilter = searchString;

            var ps = from s in db.Events where s.Status == true select s;

            if (!String.IsNullOrEmpty(searchString))
            {
                ps = ps.Where(s => s.NameEvent.Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ps = ps.OrderByDescending(s => s.NameEvent);
                    break;
                case "date":
                    ps = ps.OrderBy(s => s.StartTime);
                    break;
                case "date_desc":
                    ps = ps.OrderByDescending(s => s.StartTime);
                    break;
                case "dateEnd":
                    ps = ps.OrderBy(s => s.EndTime);
                    break;
                case "dateEnd_desc":
                    ps = ps.OrderByDescending(s => s.EndTime);
                    break;
                
                default:  // Name ascending 
                    ps = ps.OrderBy(s => s.NameEvent);
                    break;
            }


            //int pageSize = 10;
            int pageSize = (size ?? 5);
            int pageNumber = (page ?? 1);
            return View(ps.ToPagedList(pageNumber, pageSize));
            //return View(ps.ToPagedList(pageNumber, pageSize));
            //return View(db.Events.ToList());
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

        // GET: Events/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdEvent,NameEvent,Img,NumberOfParticipants,StartTime,EndTime,Prize,Status")] Event @event, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string path = Server.MapPath("~/UploadedFiles/Event");
                    string fileName = Path.GetFileName(image.FileName);

                    string fullPath = Path.Combine(path, fileName);

                    image.SaveAs(fullPath);
                    @event.Img = image.FileName;
                }
                @event.NameEvent = @event.NameEvent.ToString().Trim();
                if (@event.Prize != null)
                {
                    @event.Prize = @event.Prize.ToString().Trim();
                }

                @event.Status = true;

                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(@event);
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
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdEvent,NameEvent,Img,NumberOfParticipants,StartTime,EndTime,Prize,Status")] Event @event)
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
            @event.Status = false;
            db.Entry(@event).State = EntityState.Modified;
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
