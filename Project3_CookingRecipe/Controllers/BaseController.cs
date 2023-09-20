using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project3_CookingRecipe.Controllers
{
    public class BaseController : Controller
    {
        // GET: Base
        protected override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            if (Session["UserName"] == null)
            {
                filterContext.Result = new RedirectToRouteResult(
                   new System.Web.Routing.RouteValueDictionary(new { Controller = "LoginUser", Action = "Login" })
                );
            }
            base.OnActionExecuting(filterContext);
        }
    }
}