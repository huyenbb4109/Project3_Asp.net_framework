using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project3_CookingRecipe.Controllers
{
    public class HomeAdminController : Controller
    {
        // GET: HomeAdmin
        public ActionResult Index()
        {
            return View();
        }
    }
}