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
    public class CookingRecipesController : Controller
    {
        private DatabaseCookingJameEntities1 db = new DatabaseCookingJameEntities1();

        // GET: CookingRecipes
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
            ViewBag.Date = sortOrder == "date" ? "date_desc" : "date";
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

            var ps = from s in db.CookingRecipes where s.Status == true select s;//sua
            if (!String.IsNullOrEmpty(searchString))
            {
                ps = ps.Where(s => s.NameFood.Contains(searchString) || s.DateSubmit.ToString().Contains(searchString));
            }
            switch (sortOrder)
            {
                case "name_desc":
                    ps = ps.OrderByDescending(s => s.NameFood);
                    break;
                case "date":
                    ps = ps.OrderBy(s => s.DateSubmit);
                    break;
                case "date_desc":
                    ps = ps.OrderByDescending(s => s.DateSubmit);
                    break;
                //case "fullname":
                //    user = user.OrderBy(u => u.UserName);
                //    break;
                //case "fullname_desc":
                //    user = user.OrderByDescending(u => u.UserName);
                //    break;
                default:  // Name ascending 
                    ps = ps.OrderBy(s => s.NameFood);
                    break;
            }


            //int pageSize = 10;
            int pageSize = (size ?? 5);
            int pageNumber = (page ?? 1);
            return View(ps.ToPagedList(pageNumber, pageSize));
            //var cookingRecipes = db.CookingRecipes.Include(c => c.Event).Include(c => c.User);
            //return View(cookingRecipes.ToList());
        }

        // GET: CookingRecipes/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookingRecipe cookingRecipe = db.CookingRecipes.Find(id);
            if (cookingRecipe == null)
            {
                return HttpNotFound();
            }
            return View(cookingRecipe);
        }
        public ActionResult Detail1(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookingRecipe cookingRecipe = db.CookingRecipes.Find(id);
            if (cookingRecipe == null)
            {
                return HttpNotFound();
            }
            return View(cookingRecipe);
        }

        // GET: CookingRecipes/Create
        public ActionResult Create()
        {
            ViewBag.IdEvent = new SelectList(db.Events, "IdEvent", "NameEvent");
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "FullName");
            return View();
        }

        // POST: CookingRecipes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create([Bind(Include = "IdRecipe,NameFood,Img,Describe,Ingredients,IdUser,IdEvent,DateSubmit,Steps,Status")] CookingRecipe cookingRecipe, HttpPostedFileBase image)
        {
            
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string path = Server.MapPath("~/UploadedFiles");
                    string fileName = Path.GetFileName(image.FileName);

                    string fullPath = Path.Combine(path, fileName);

                    image.SaveAs(fullPath);
                    cookingRecipe.Img = image.FileName;
                }
                cookingRecipe.NameFood = cookingRecipe.NameFood.ToString().Trim();
                if(cookingRecipe.Describe != null)
                {
                    cookingRecipe.Describe = cookingRecipe.Describe.ToString().Trim();
                }
                cookingRecipe.Ingredients = cookingRecipe.Ingredients.ToString().Trim();
                cookingRecipe.Steps = cookingRecipe.Steps.ToString().Trim();

                cookingRecipe.DateSubmit = DateTime.Now;
                cookingRecipe.Status = true;

                db.CookingRecipes.Add(cookingRecipe);
                db.SaveChanges();
                return RedirectToAction("Detail1", "CookingRecipes", new { id = cookingRecipe.IdRecipe });
            }
            else
            {
                ViewBag.Result = "Create failed recipes!";
            }

            ViewBag.IdEvent = new SelectList(db.Events, "IdEvent", "NameEvent", cookingRecipe.IdEvent);
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "FullName", cookingRecipe.IdUser);
            return View(cookingRecipe);
        }

        // GET: CookingRecipes/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookingRecipe cookingRecipe = db.CookingRecipes.Find(id);
            if (cookingRecipe == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdEvent = new SelectList(db.Events, "IdEvent", "NameEvent", cookingRecipe.IdEvent);
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "FullName", cookingRecipe.IdUser);
            return View(cookingRecipe);
        }

        // POST: CookingRecipes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit([Bind(Include = "IdRecipe,NameFood,Img,Describe,Ingredients,IdUser,IdEvent,DateSubmit,Steps,Status")] CookingRecipe cookingRecipe, HttpPostedFileBase image)
        {
            if (ModelState.IsValid)
            {
                if (image != null && image.ContentLength > 0)
                {
                    string path = Server.MapPath("~/UploadedFiles");
                    string fileName = Path.GetFileName(image.FileName);

                    string fullPath = Path.Combine(path, fileName);

                    image.SaveAs(fullPath);
                    cookingRecipe.Img = image.FileName;
                }
                cookingRecipe.NameFood = cookingRecipe.NameFood.ToString().Trim();
                if (cookingRecipe.Describe != null)
                {
                    cookingRecipe.Describe = cookingRecipe.Describe.ToString().Trim();
                }
                cookingRecipe.Ingredients = cookingRecipe.Ingredients.ToString().Trim();
                cookingRecipe.Steps = cookingRecipe.Steps.ToString().Trim();

                cookingRecipe.DateSubmit = DateTime.Now;
                cookingRecipe.Status = true;

                db.Entry(cookingRecipe).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                ViewBag.Result = "Edit failed recipes!";
            }
            ViewBag.IdEvent = new SelectList(db.Events, "IdEvent", "NameEvent", cookingRecipe.IdEvent);
            ViewBag.IdUser = new SelectList(db.Users, "IdUser", "FullName", cookingRecipe.IdUser);
            return View(cookingRecipe);
        }

        // GET: CookingRecipes/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            CookingRecipe cookingRecipe = db.CookingRecipes.Find(id);
            if (cookingRecipe == null)
            {
                return HttpNotFound();
            }
            return View(cookingRecipe);
        }

        // POST: CookingRecipes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            CookingRecipe cookingRecipe = db.CookingRecipes.Find(id);
            cookingRecipe.Status = false;
            db.Entry(cookingRecipe).State = EntityState.Modified;
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

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult UploadImage(HttpPostedFileBase upload, string CKEditorFuncNum, string CKEditor, string langCode)
        {
            string url; // url to return
            string message; // message to display (optional)

            // path of the image
            string path = Server.MapPath("~/UploadedFiles/Steps");

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);


            // here logic to upload image
            string ImageName = upload.FileName;
            upload.SaveAs(System.IO.Path.Combine(path, ImageName));


            // and get file path of the image
            // will create http://localhost:9999/Content/Images/Uploads/ImageName.jpg
            url = Request.Url.GetLeftPart(UriPartial.Authority) + "/UploadedFiles/Steps" + ImageName;

            // passing message success/failure
            message = "Image was saved correctly";

            // since it is an ajax request it requires this string
            string output = @"<html><body><script>window.parent.CKEDITOR.tools.callFunction(" + CKEditorFuncNum + ", \"" + url + "\", \"" + message + "\");</script></body></html>";
            return Content(output);
        }

    }
}
