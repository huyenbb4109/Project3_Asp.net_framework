using Project3_CookingRecipe.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project3_CookingRecipe.Controllers
{
    public class LoginKHController : Controller
    {
        // GET: LoginKH
        DatabaseCookingJameEntities1 d_b = new DatabaseCookingJameEntities1();
        public ActionResult Index()
        {
            return View(d_b.Users.ToList());
        }
        public ActionResult Login_KH()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login_KH(User _user)
        {

            var check = d_b.Users.Where(s => s.UserName.Equals(_user.UserName)
            && s.Password.Equals(_user.Password)).FirstOrDefault();
            if (check == null)
            {
                _user.LoginErrorMessage = "Erro User or password !";
                return View("Index", _user);
            }
            else
            {
                Session["IdUser"] = check.IdUser.ToString();
                Session["UserName"] = check.UserName.ToString();

                return RedirectToAction("About", "Home");
            }


        }

        [HttpGet]
        public ActionResult Register()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Register(User _user)
        {
            if (ModelState.IsValid)
            {
                var check = d_b.Users.FirstOrDefault(s => s.Email == _user.Email);
                if (check != null)
                {
                    d_b.Configuration.ValidateOnSaveEnabled = false;
                    d_b.Users.Add(_user);
                    d_b.SaveChanges();
                    return RedirectToAction("Index");
                }
                else
                {
                    ViewBag.error = "Email already exists! Use another email please!";
                    return View();
                }
            }
            return View();
        }
    }
}