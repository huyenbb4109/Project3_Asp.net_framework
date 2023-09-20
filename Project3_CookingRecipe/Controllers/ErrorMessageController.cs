using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project3_CookingRecipe.Controllers
{
    public class ErrorMessageController : Controller
    {
        // GET: ErrorMessage
        public ActionResult NoRight()
        {
            return View();
        }
    }
}