using Project3_CookingRecipe.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Project3_CookingRecipe.Controllers
{
    public class LoginUserController : Controller
    {
        // GET: LoginUset
        DatabaseCookingJameEntities1 d_b = new DatabaseCookingJameEntities1();
        public ActionResult Index()
        {
            return View(d_b.Users.ToList());
        }
        public ActionResult Login()
        {
            return View();
        }
        [HttpPost]

        public ActionResult Login(User _user)
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
                Session["FullName"] = check.FullName.ToString();
                ViewBag.FullName = Session["FullName"];
                ViewBag.IdUser = Session["IdUser"];

                return RedirectToAction("Index", "HomeAdmin");
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
        public ActionResult InforUser()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult AddItem()
        {

            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Update(int IdUser)
        {

            User manager = d_b.Users.Find(IdUser);

            return View(manager);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "IdUser,FullName")] User model)
        {

            if (string.IsNullOrEmpty(model.FullName) == true)
            {
                ModelState.AddModelError("", "Fullname must not be left blank");
                return View(model);
            }
            if (string.IsNullOrEmpty(model.Phone) == true)
            {
                ModelState.AddModelError("", "Fullname must not be left blank");
                return View(model);
            }
            if (ModelState.IsValid)
            {
                d_b.Entry(model).State = EntityState.Modified;
                d_b.SaveChanges();
                return RedirectToAction("Index");
            }

            if (model.IdUser > 0)
            {
                return RedirectToAction("InforUser", "LoginUser");
            }
            else
            {
                ModelState.AddModelError("", "Can't save to db");
                return View(model);
            }
        }
    }
}