using Project3_CookingRecipe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;

namespace Project3_CookingRecipe.Controllers
{
    public class HomeController : Controller
    {
        private DatabaseCookingJameEntities1 db = new DatabaseCookingJameEntities1();
        public ActionResult Index()
        {
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult TrangChu()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Recipe()
        {
            ViewBag.Message = "Your recipe page.";

            return View(db.CookingRecipes.ToList());
        }
        public ActionResult Event()
        {
            ViewBag.Message = "Your event page.";

            return View(db.Events.ToList());

        }
        public ActionResult EventSingle(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event event1 = db.Events.Find(id);
            if (event1 == null)
            {
                return HttpNotFound();
            }
            return View(event1);
        }
        public ActionResult DetailRecipe(int? id)
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
        public ActionResult Tips()
        {
            ViewBag.Message = "Your tip page.";

            return View(db.Tips.ToList());

        }
    }
}